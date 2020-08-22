using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Text;
using Axle.Verification;


namespace Axle.Configuration
{
    public sealed class LayeredConfigManager : IConfigManager
    {
        private class PreloadedConfigSource : IConfigSource
        {
            private readonly IConfiguration _config;

            public PreloadedConfigSource(IConfiguration config) => _config = config;

            public IConfiguration LoadConfiguration() => _config;
            
        }
        internal sealed class LayeredConfiguration : IConfiguration
        {
            private readonly IEnumerable<IConfiguration> _configurations;

            public LayeredConfiguration(IEnumerable<IConfiguration> configurations)
            {
                _configurations = configurations;
            }

            public IEnumerable<IConfigSetting> this[string key]
            {
                get
                {
                    key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                    var results = _configurations
                        .SelectMany(x => x[key].Where(y => y is IConfigSection))
                        .ToList();
                    if (results.Count == 0)
                    {
                        results = (_configurations
                            .Select(x => x[key].ToArray())
                            .FirstOrDefault(x => x.Length > 0 && x.All(y => !(y is IConfigSection))) ?? new IConfigSetting[0])
                            .ToList();
                    }
                    return results;
                }
            }

            public IEnumerable<string> Keys => _configurations.SelectMany(c => c.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

            public string Name => string.Empty;

            public CharSequence Value => null;
        }

        private readonly IConfiguration[] _configs;

        private LayeredConfigManager(IConfiguration[] configs)
        {
            _configs = configs;
        }
        public LayeredConfigManager() : this(new IConfiguration[0]) { }

        public LayeredConfigManager Append(IConfigSource source)
        {
            source.VerifyArgument(nameof(source)).IsNotNull();
            var cfg = source.LoadConfiguration();
            if (cfg == null)
            {
                return this;
            }
            return new LayeredConfigManager(new[] { cfg }.Union(_configs).ToArray());
        }

        public LayeredConfigManager Append(IConfiguration config)
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            return new LayeredConfigManager(new[] { config }.Union(_configs).ToArray());
        }

        /// <inheritdoc />
        public IConfiguration LoadConfiguration() => _configs.Length == 0 ? null : new LayeredConfiguration(_configs);
    }
}