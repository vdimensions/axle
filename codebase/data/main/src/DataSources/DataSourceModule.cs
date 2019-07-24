using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.DataSources
{
    [Module]
    [Requires(typeof(DataModule))]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class DataSourceModule
    {
        private readonly IDbServiceProviderRegistry _dbServiceProviders;

        public DataSourceModule(IDbServiceProviderRegistry dbServiceProviderRegistry)
        {
            _dbServiceProviders = dbServiceProviderRegistry;
        }
    }
}
