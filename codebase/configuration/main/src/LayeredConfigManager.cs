using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;


namespace Axle.Configuration
{
    public sealed class LayeredConfigManager : IConfigManager
    {
        internal sealed class LayeredConfiguration : IConfiguration
        {
            private readonly IEnumerable<IConfiguration> _configurations;

            public LayeredConfiguration(IEnumerable<IConfiguration> configurations)
            {
                _configurations = configurations;
            }

            public IConfigSetting this[string key]
            {
                get
                {
                    key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                    return _configurations.Select(x => x[key]).FirstOrDefault(x => x != null);
                }
            }

            public IEnumerable<string> Keys => _configurations.SelectMany(c => c.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

            public string Name => string.Empty;

            public string Value => null;
        }

        private readonly IEnumerable<IConfiguration> _configs;

        private LayeredConfigManager(IEnumerable<IConfiguration> configs)
        {
            _configs = configs;
        }
        public LayeredConfigManager() : this(new List<IConfiguration>()) { }

        public LayeredConfigManager Prepend(IConfigSource source)
        {
            //
            // NB prepends configs to the end, as they are being prioritized by order
            //
            source.VerifyArgument(nameof(source)).IsNotNull();
            var newConfigs = _configs.Union(new[] {source.LoadConfiguration()});
            return new LayeredConfigManager(newConfigs);
        }
        public LayeredConfigManager Append(IConfigSource source)
        {
            //
            // NB appends configs to the beginning, as they are being prioritized by order
            //
            source.VerifyArgument(nameof(source)).IsNotNull();
            var newConfigs = new[] {source.LoadConfiguration()}.Union(_configs);
            return new LayeredConfigManager(newConfigs);
        }

        public IConfiguration LoadConfiguration() => new LayeredConfiguration(_configs);
    }
}