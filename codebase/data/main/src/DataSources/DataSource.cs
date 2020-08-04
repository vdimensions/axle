using System;
using System.Data;
using System.Globalization;
using Axle.Data.DataSources.Resources;
using Axle.Resources;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    public class DataSource : IDataSource
    {
        private readonly IDbServiceProvider _serviceProvider;
        private readonly ResourceManager _resourceManager;

        internal DataSource(string name, IDbServiceProvider serviceProvider, string connectionString, ResourceManager resourceManager)
        {
            Name = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            ConnectionString = Verifier.IsNotNull(Verifier.VerifyArgument(connectionString, nameof(connectionString)));
            _serviceProvider = Verifier.IsNotNull(Verifier.VerifyArgument(serviceProvider, nameof(serviceProvider))).Value;
            _resourceManager = Verifier.IsNotNull(Verifier.VerifyArgument(resourceManager, nameof(resourceManager)));
        }

        /// <inheritdoc />
        public IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction) 
            => _serviceProvider.GetDbParameterBuilder().CreateParameter(name, direction);

        /// <inheritdoc />
        public IDataSourceConnection OpenConnection() 
            => new DataSourceConnection(_serviceProvider, this);

        private ICommandBuilder BuildCommand(string commandText, CommandType commandType) 
            => new DataSourceCommandBuilder(_serviceProvider, this, commandType, commandText);

        private ICommandBuilder BuildScriptedCommand(string bundle, string scriptPath, CommandType commandType)
        {
            var res = _resourceManager.Load(bundle, scriptPath, CultureInfo.InvariantCulture);
            if (res != null && res is SqlScriptSourceInfo scriptSourceInfo)
            {
                var commandText = scriptSourceInfo.Value.ResolveScript(_serviceProvider);
                return BuildCommand(commandText, commandType);
            }
            throw new InvalidOperationException(string.Format("Could not resolve sql script {0}.", scriptPath));
        }

        //private ICommandBuilder BuildStoredProcedureCall(string bundleName, string commandName)
        //{
        //    return BuildCommand(CommandType.StoredProcedure, bundleName, commandName);
        //}
        private ICommandBuilder BuildStoredProcedureCall(string commandText) => BuildCommand(commandText, CommandType.StoredProcedure);

        /// <inheritdoc />
        public IDataSourceCommand GetCommand(string commandText, CommandType commandType, BuildCommandCallback buildCommandCallback)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(buildCommandCallback, nameof(buildCommandCallback)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(commandText, nameof(commandText)));
            var b = buildCommandCallback(BuildCommand(commandText, commandType));
            return ((ICommandBuilderResult) b).Build();
        }

        /// <inheritdoc />
        public IDataSourceCommand GetScript(string bundle, string scriptPath, CommandType commandType, BuildCommandCallback buildCommandCallback)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(bundle, nameof(bundle)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(scriptPath, nameof(scriptPath)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildCommandCallback, nameof(buildCommandCallback)));
            var b = buildCommandCallback(BuildScriptedCommand(bundle, scriptPath, commandType));
            return ((ICommandBuilderResult) b).Build();
        }

        public string Name { get; }
        /// <inheritdoc />
        public string ConnectionString { get; }
        /// <inheritdoc />
        public string DialectName => _serviceProvider.DialectName;
    }
}