using System;
using System.Data;
using System.Globalization;
using Axle.Data.DataSources.Resources;
using Axle.Resources;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    internal sealed class DataSourceConnection : IDataSourceConnection
    {
        private readonly IDbServiceProvider _provider;
        private readonly IDbConnection _connection;
        private readonly ResourceManager _resourceManager;
        private IDataSourceTransaction _currentTransaction;

        public DataSourceConnection(IDbServiceProvider provider, IDataSource dataSource, ResourceManager resourceManager)
        {
            _provider = provider;
            DataSource = dataSource;
            _resourceManager = resourceManager;
            _connection = provider.CreateConnection(dataSource.ConnectionString);
            _connection.Open();
        }

        public IDataSourceTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already started for this connection.");
            }

            var dbTransaction = _provider.CreateTransaction(_connection, isolationLevel);
            return _currentTransaction = new DataSourceTransaction(dbTransaction, this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
                _connection?.Dispose();
            }
        }
        void IDisposable.Dispose() => Dispose(true);

        private ICommandBuilder BuildCommand(string commandText, CommandType commandType)
        {
            return new DataSourceCommandBuilder(_provider, this, commandType, commandText);
        }

        private ICommandBuilder BuildScriptedCommand(string bundle, string scriptPath, CommandType commandType)
        {
            var res = _resourceManager.Load(bundle, scriptPath, CultureInfo.InvariantCulture);
            if (res.HasValue && res.Value is SqlScriptSourceInfo scriptSourceInfo)
            {
                var commandText = scriptSourceInfo.Value.ResolveScript(_provider);
                return BuildCommand(commandText, commandType);
            }
            throw new InvalidOperationException(string.Format("Could not resolve sql script {0}.", scriptPath));
        }

        //private ICommandBuilder BuildStoredProcedureCall(string bundleName, string commandName)
        //{
        //    return BuildCommand(CommandType.StoredProcedure, bundleName, commandName);
        //}
        private ICommandBuilder BuildStoredProcedureCall(string commandText) => BuildCommand(commandText, CommandType.StoredProcedure);

        public IDataSourceCommand GetCommand(string commandText, CommandType commandType, Func<ICommandBuilder, ICommandBuilder> buildCommandCallback)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(buildCommandCallback, nameof(buildCommandCallback)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(commandText, nameof(commandText)));
            var b = buildCommandCallback(BuildCommand(commandText, commandType));
            return ((ICommandBuilderResult) b).Build();
        }

        public IDataSourceCommand GetScript(string bundle, string scriptPath, CommandType commandType, Func<ICommandBuilder, ICommandBuilder> buildCommandCallback)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(bundle, nameof(bundle)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(scriptPath, nameof(scriptPath)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildCommandCallback, nameof(buildCommandCallback)));
            var b = buildCommandCallback(BuildScriptedCommand(bundle, scriptPath, commandType));
            return ((ICommandBuilderResult) b).Build();
        }

        public IDataSourceTransaction CurrentTransaction => _currentTransaction;
        public IDataSource DataSource { get; }
        IDbConnection IDataSourceResource<IDbConnection>.WrappedInstance => _connection;
    }
}