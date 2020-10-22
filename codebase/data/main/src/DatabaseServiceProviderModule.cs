using Axle.Modularity;

namespace Axle.Data
{
    /// <summary>
    /// An abstract module class to serve as a base for implementing a module that registers
    /// <see cref="IDbServiceProvider"/>
    /// </summary>
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
