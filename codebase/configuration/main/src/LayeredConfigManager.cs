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

            public IEnumerable<IConfigSetting> this[string key]
            {
                get
                {
                    key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                    var results = _configurations
                        .SelectMany(x => x[key].Where(y => y != null))
                        .ToList();
                    // in case we have found a collection of simple setting values, take only the newest
                    var overridingSetting = results.FirstOrDefault(x => !(x is IConfigSection));
                    return overridingSetting != null
                        ? new[] { overridingSetting }
                        : (IEnumerable<IConfigSetting>) results;
                }
            }

            public IEnumerable<string> Keys => _configurations.SelectMany(c => c.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

            public string Name => string.Empty;

            public string Value => null;
        }

        private readonly IConfiguration[] _configs;

        private LayeredConfigManager(IConfiguration[] configs)
        {
            _configs = configs;
        }
        public LayeredConfigManager() : this(new IConfiguration[0]) { }

        public LayeredConfigManager Append(IConfigSource source)
        {
            //
            // NB appends configs to the beginning, as they are being prioritized by order
            //
            source.VerifyArgument(nameof(source)).IsNotNull();
            var cfg = source.LoadConfiguration();
            if (cfg == null)
            {
                return this;
            }
            return new LayeredConfigManager(new[] { cfg }.Union(_configs).ToArray());
        }

        /// <inheritdoc />
        public IConfiguration LoadConfiguration() => _configs.Length == 0 ? null : new LayeredConfiguration(_configs);
    }
}