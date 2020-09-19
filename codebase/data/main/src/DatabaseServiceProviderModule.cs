using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    [ProvidesForDataModule]
    public abstract class DatabaseServiceProviderModule
    {
        protected DatabaseServiceProviderModule(IDbServiceProvider provider)
        {
            Provider = provider;
        }

        internal IDbServiceProvider Provider { get; }
    }
}
