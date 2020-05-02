using Axle.Configuration;

namespace Axle
{
    
    partial class Application
    {
        internal class PreloadedConfigSource : IConfigSource
        {
            private readonly IConfiguration _config;

            public PreloadedConfigSource(IConfiguration config) => _config = config;

            public IConfiguration LoadConfiguration() => _config;
        }

        internal static LayeredConfigManager Configure(
            LayeredConfigManager configManager,
            string configFileName,
            IConfigurationStreamProvider configStreamProvider, 
            string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName))
            {
                return configManager
                    .Append(new YamlConfigSource(configFileName, configStreamProvider))
                    // TODO: JSON
                    .Append(new PropertiesConfigSource(configFileName, configStreamProvider))
                    ;
            }
            return configManager
                .Append(new YamlConfigSource(configFileName, configStreamProvider, environmentName))
                // TODO: JSON
                .Append(new PropertiesConfigSource(configFileName, configStreamProvider, environmentName))
                ;
        }
    }
}