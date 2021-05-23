using Axle.Configuration;

namespace Axle.Application
{
    partial class Application
    {
        internal static LayeredConfigManager Configure(
            LayeredConfigManager configManager,
            string configFileName,
            IConfigurationStreamProvider configStreamProvider, 
            string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName))
            {
                return configManager
                    .Append(new YamlFileConfigSource(configFileName, configStreamProvider))
                    // TODO: JSON
                    .Append(new PropertiesFileConfigSource(configFileName, configStreamProvider))
                    ;
            }
            return configManager
                .Append(new YamlFileConfigSource(configFileName, configStreamProvider, environmentName))
                // TODO: JSON
                .Append(new PropertiesFileConfigSource(configFileName, configStreamProvider, environmentName))
                ;
        }
    }
}