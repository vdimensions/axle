using System;
using System.IO;
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
            Func<string, Stream> configStreamProvider, 
            string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName))
            {
                return configManager
                    .Append(new YamlConfigSource(configStreamProvider))
                    // TODO: JSON
                    .Append(new PropertiesConfigSource(configStreamProvider))
                    ;
            }
            return configManager
                .Append(new YamlConfigSource(configStreamProvider, environmentName))
                // TODO: JSON
                .Append(new PropertiesConfigSource(configStreamProvider, environmentName))
                ;
        }
    }
}