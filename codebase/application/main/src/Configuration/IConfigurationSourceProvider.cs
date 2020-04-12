using Axle.Modularity;

namespace Axle.Configuration
{
    [Requires(typeof(ConfigSourceRegistry))]
    [ReportsTo(typeof(ConfigurationModule))]
    public interface IConfigurationSourceProvider
    {
        void RegisterConfigurationSources(IConfigurationSourceRegistry registry);
    }
}