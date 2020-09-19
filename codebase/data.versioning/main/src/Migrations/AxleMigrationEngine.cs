using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Axle.Data.DataSources;
using Axle.Logging;

namespace Axle.Data.Versioning.Migrations
{
    internal class AxleMigrationEngine : IMigrationEngine
    {
        private HashSet<string> _migrationSupportPerDataSource = new HashSet<string>(StringComparer.Ordinal);
        
        private void InitializeMigrationSupport(IDataSource dataSource)
        {
            if (!_migrationSupportPerDataSource.Add(dataSource.ConnectionString))
            {
                return;
            }
            
            var createCommand = dataSource.GetScript(MigratorDbScriptsModule.Bundle, "create");
            using (var connection = dataSource.OpenConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                Logger.Trace("Ensuring required database objects for migration changelog support are created...");
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
            IDataSourceCommand selectMigrationCountQuery,
            IDataSourceCommand insertMigrationQuery, 
            string name, 
            out int migrationId)
        {
            migrationId = -1;
            using (var connection = dataSource.OpenConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var count = selectMigrationCountQuery.ExecuteScalar<int>(
                    connection,
                    dataSource.CreateInputParameter("name", b => b.SetValue(name).SetSize(name.Length))
                );
                if (count != 0)
                {
                    // migration already ran
                    Logger.Trace("Migration '{0}' has already been executed. ", name);
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
                    Logger.Debug("Record created for migration '{0}'. ", name);
                    return true;
                }
                catch (Exception e)
                {
                    Logger.Error($"Error creating migration '{name}''. Rolling back changes...", e);
                    transaction.Rollback();
                    throw new MigrationEngineException(string.Format("Could not execute migration '{0}'.", name), e);
                }
            }
        }
        
        private void EndExecutingMigration(
            IDataSource dataSource, 
            IDataSourceCommand updateMigrationQuery, 
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
            var hasError = false;
            var id = -1;
            var operation = Operation.Apply;
            var isReversible = false;//migration is IReversibleMigration;

            InitializeMigrationSupport(dataSource);

            var selectMigrationCountQuery = dataSource.GetScript(MigratorDbScriptsModule.Bundle, "count");
            var insertMigrationQuery = dataSource.GetScript(MigratorDbScriptsModule.Bundle, "insert");
            var updateMigrationQuery = dataSource.GetScript(MigratorDbScriptsModule.Bundle, "update");
            
            var queryString = new StreamReader(migrationStream).ReadToEnd();
            
            if (BeginExecutingMigration(dataSource, selectMigrationCountQuery, insertMigrationQuery, name, out var migrationId))
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
                            hasError = true;
                            status = MigrationStatus.Failed;
                        }
                    }
                }
                finally
                {
                    EndExecutingMigration(dataSource, updateMigrationQuery, migrationId, name, status, migrationStopWatch.ElapsedMilliseconds, failureCause);
                    migrationStopWatch.Stop();
                }
            }
        }

        internal ILogger Logger { get; set; }
    }
}
