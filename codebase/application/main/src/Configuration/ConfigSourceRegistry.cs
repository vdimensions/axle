using Axle.Modularity;

namespace Axle.Configuration
{
    [Module]
    internal sealed class ConfigSourceRegistry : IConfigurationSourceRegistry
    {
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IConfigurationSourceProvider configurationSourceProvider)
        {
            configurationSourceProvider.RegisterConfigurationSources(this);
        }
    }
}