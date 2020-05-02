using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Configuration.Text.Documents;
using Axle.Text.Documents.Yaml;
using Axle.Verification;

namespace Axle.Configuration
{
    public sealed class YamlConfigSource : IConfigSource
    {
        private const string DefaultConfigFileName = "{0}{1}.yml";
        private const string AlternateConfigFileName = "{0}{1}.yaml";
        
        private readonly IList<string> _fileNames;
        private readonly IConfigurationStreamProvider _configStreamProvider;

        private YamlConfigSource(string fileName, IConfigurationStreamProvider configStreamProvider, string env, params string[] fileNameFormats)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(fileName, nameof(fileName)));
            _configStreamProvider = configStreamProvider;
            var envFormat = string.IsNullOrEmpty(env) ? string.Empty : $".{env}";
            _fileNames = fileNameFormats.Select(f => string.Format(f, fileName, envFormat)).ToList();
        }
        public YamlConfigSource(string fileName, IConfigurationStreamProvider configStreamProvider, string env)
            : this(fileName, configStreamProvider, env, DefaultConfigFileName, AlternateConfigFileName) { }
        public YamlConfigSource(string fileName, IConfigurationStreamProvider configStreamProvider)
            : this(fileName, configStreamProvider, null, DefaultConfigFileName, AlternateConfigFileName) { }

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