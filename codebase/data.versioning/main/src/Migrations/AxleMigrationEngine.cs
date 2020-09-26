using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Axle.Data.DataSources;
using Axle.Logging;

namespace Axle.Data.Versioning.Migrations
{
    internal class AxleMigrationEngine : IMigrationEngine
    {
        private const int LockID = 1;
        
        private HashSet<string> _migrationSupportPerDataSource = new HashSet<string>(StringComparer.Ordinal);
        
        private void InitializeMigrationSupport(IDataSource dataSource)
        {
            if (!_migrationSupportPerDataSource.Add(dataSource.ConnectionString))
            {
                return;
            }
            
            var existsCommand = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "exists");
            var createCommand = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "create");
            using (var connection = dataSource.OpenConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                if (existsCommand.ExecuteScalar<int>(connection) > 0)
                {
                    return;
                }

                Logger.Trace("Ensuring required database objects for migration changelog support are created on datasource {0}...", dataSource.Name);
                try
                {
                    createCommand.ExecuteNonQuery(connection);
                    transaction.Commit();
                    Logger.Trace("Database migration changelog support is ready. ");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Logger.Fail(e, "An error has occurred while creating migration changelog. See inner exception for more details.");
                    throw;
                }
            }
        }

        private bool BeginExecutingMigration(
            IDataSource dataSource,
            IDataSourceCommand lockQuery,
            IDataSourceCommand selectMigrationCountQuery,
            IDataSourceCommand insertMigrationQuery,
            string name,
            out int migrationId)
        {
            migrationId = -1;
            var generatedLockName = Guid.NewGuid().ToString();
            var persistedLockName = generatedLockName;
            do
            {
                if (string.IsNullOrEmpty(persistedLockName))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                using (var connection = dataSource.OpenConnection())
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    persistedLockName = lockQuery.ExecuteScalar<string>(
                        connection,
                        dataSource.CreateInputParameter("id", b => b.SetValue(LockID)),
                        dataSource.CreateInputParameter("name", b => b.SetValue(generatedLockName))
                    );
                    
                    if (!StringComparer.Ordinal.Equals(generatedLockName, persistedLockName))
                    {
                        persistedLockName = null;
                        Logger.Debug("An active migration lock exists on the datasource. Retying in a few seconds... ");
                        continue; 
                    }

                    var count = selectMigrationCountQuery.ExecuteScalar<int>(
                        connection,
                        dataSource.CreateInputParameter("name", b => b.SetValue(name).SetSize(name.Length))
                    );
                    if (count != 0)
                    {
                        // migration already ran
                        Logger.Debug("Migration '{0}' has already been executed against '{1}' datasource. ", name,
                            dataSource.Name);
                        return false;
                    }

                    Logger.Debug("Executing migration '{0}'. ", name);

                    var status = MigrationStatus.NotRan;
                    var duration = 0L;
                    try
                    {
                        var status1 = status;
                        var duration1 = duration;
                        migrationId = insertMigrationQuery.ExecuteScalar<int>(
                            connection,
                            dataSource.CreateInputParameter("name", b => b.SetValue(name).SetSize(name.Length)),
                            dataSource.CreateInputParameter("status", b => b.SetValue((byte) status1)),
                            dataSource.CreateInputParameter("executionDuration", b => b.SetValue(duration1)),
                            dataSource.CreateInputParameter("datePerformed", b => b.SetValue(DateTime.UtcNow))
                        );
                        transaction.Commit();
                        Logger.Debug("Record created for migration '{0}' on datasource '{1}'. ", name, dataSource.Name);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Logger.Error(
                            $"Error creating migration '{name}' on datasource '{dataSource.Name}'. Rolling back changes...",
                            e);
                        transaction.Rollback();
                        throw new MigrationEngineException(string.Format("Could not execute migration '{0}'.", name), e);
                    }
                }
            } 
            // ReSharper disable once ExpressionIsAlwaysNull
            while (!StringComparer.Ordinal.Equals(generatedLockName, persistedLockName));

            // ReSharper disable once ExpressionIsAlwaysNull
            return StringComparer.Ordinal.Equals(generatedLockName, persistedLockName);
        }
        
        private void EndExecutingMigration(
            IDataSource dataSource, 
            IDataSourceCommand updateMigrationQuery, 
            IDataSourceCommand unlockQuery,
            int id,
            string name,
            MigrationStatus status,
            long duration,
            Exception failureCause)
        {
            using (var connection = dataSource.OpenConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                updateMigrationQuery.ExecuteNonQuery(
                    connection,
                    dataSource.CreateInputParameter("id", b => b.SetValue(id)),
                    dataSource.CreateInputParameter("status", b => b.SetValue((byte) status)),
                    dataSource.CreateInputParameter("executionDuration", b => b.SetValue(duration)),
                    dataSource.CreateInputParameter("datePerformed", b => b.SetValue(DateTime.UtcNow)));

                unlockQuery.ExecuteNonQuery(
                    connection,
                    dataSource.CreateInputParameter("id", b => b.SetValue(LockID)));
                transaction.Commit();

                switch (status)
                {
                    case MigrationStatus.Successful:
                        Logger.Trace("Migration '{0}' successfully executed in {1} ms. ", name, duration);
                        break;
                    case MigrationStatus.Failed:
                        var errorMessage = $"Migration '{name}' failed. ";
                        Logger.Error(errorMessage, failureCause);
                        throw new MigrationEngineException(errorMessage, failureCause);
                }
            }
        }

        public void Migrate(IDataSource dataSource, string name, Stream migrationStream)
        {
            InitializeMigrationSupport(dataSource);

            var lockQuery = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "lock");
            var unlockQuery = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "unlock");
            var selectMigrationCountQuery = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "count");
            var insertMigrationQuery = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "insert");
            var updateMigrationQuery = dataSource.GetScript(MigratorScriptProviderModule.Bundle, "update");
            
            var queryString = new StreamReader(migrationStream).ReadToEnd();
            
            if (BeginExecutingMigration(dataSource, lockQuery, selectMigrationCountQuery, insertMigrationQuery, name, out var migrationId))
            {
                var status = MigrationStatus.NotRan;
                var migrationStopWatch = Stopwatch.StartNew();
                Exception failureCause = null;
                try
                {
                    using (var migrationConnection = dataSource.OpenConnection())
                    using (var migrationTransaction = migrationConnection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            dataSource.GetCommand(queryString).ExecuteNonQuery(migrationConnection);
                            status = MigrationStatus.Successful;
                            migrationTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            migrationTransaction.Rollback();
                            failureCause = e;
                            status = MigrationStatus.Failed;
                        }
                    }
                }
                finally
                {
                    EndExecutingMigration(dataSource, updateMigrationQuery, unlockQuery, migrationId, name, status, migrationStopWatch.ElapsedMilliseconds, failureCause);
                    migrationStopWatch.Stop();
                }
            }
        }

        internal ILogger Logger { get; set; }
    }
}
