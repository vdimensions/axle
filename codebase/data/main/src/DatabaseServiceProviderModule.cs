using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    [ReportsToDataModule]
    public abstract class DatabaseServiceProviderModule
    {
        protected DatabaseServiceProviderModule(IDbServiceProvider provider)
        {
            Provider = provider;
        }

        internal IDbServiceProvider Provider { get; }
    }
}
