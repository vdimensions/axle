using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Configuration.Text.Documents;
using Axle.Text.Documents.Yaml;

namespace Axle.Configuration
{
    public sealed class YamlConfigSource : IConfigSource
    {
        private const string DefaultConfigFileName = "application{0}.yml";
        private const string AlternateConfigFileName = "application{0}.yaml";
        
        private readonly IList<string> _fileNames;
        private readonly IConfigurationStreamProvider _configStreamProvider;

        private YamlConfigSource(IConfigurationStreamProvider configStreamProvider, string env, params string[] fileNames)
        {
            _configStreamProvider = configStreamProvider;
            var envFormat = string.IsNullOrEmpty(env) ? string.Empty : $".{env}";
            _fileNames = fileNames.Select(fileName => string.Format(fileName, envFormat)).ToList();
        }
        public YamlConfigSource(IConfigurationStreamProvider configStreamProvider, string env)
            : this(configStreamProvider, env, DefaultConfigFileName, AlternateConfigFileName) { }
        public YamlConfigSource(IConfigurationStreamProvider configStreamProvider)
            : this(configStreamProvider, null, DefaultConfigFileName, AlternateConfigFileName) { }

        public IConfiguration LoadConfiguration()
        {
            var configManager = new LayeredConfigManager();
            var comparer = StringComparer.OrdinalIgnoreCase;
            var yamlReader = new YamlDocumentReader(comparer);
            foreach (var fileName in _fileNames)
            {
                configManager = configManager.Append(
                    new SafeConfigSource(
                        new StreamDocumentConfigSource(yamlReader, () => _configStreamProvider.LoadConfiguration(fileName))));
            }
            return configManager.LoadConfiguration();
        }
    }
}