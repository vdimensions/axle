using System.Data;
using Axle.Resources;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    public class DataSource : IDataSource
    {
        private readonly IDbServiceProvider _serviceProvider;
        private readonly ResourceManager _resourceManager;

        public DataSource(string name, IDbServiceProvider serviceProvider, string connectionString, ResourceManager resourceManager)
        {
            Name = StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            _serviceProvider = Verifier.IsNotNull(Verifier.VerifyArgument(serviceProvider, nameof(serviceProvider))).Value;
            ConnectionString = Verifier.IsNotNull(Verifier.VerifyArgument(connectionString, nameof(connectionString)));
            _resourceManager = Verifier.IsNotNull(Verifier.VerifyArgument(resourceManager, nameof(resourceManager)));
        }

        public IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction) => _serviceProvider.GetDbParameterBuilder().CreateParameter(name, direction);

        public IDataSourceConnection OpenConnection() => new DataSourceConnection(_serviceProvider, this, _resourceManager);

        public string Name { get; }
        public string ConnectionString { get; }
        public string DialectName => _serviceProvider.DialectName;
    }
}