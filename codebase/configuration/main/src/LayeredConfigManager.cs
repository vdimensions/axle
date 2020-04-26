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
                        .Select(x => x[key].Where(y => y != null))
                        .SelectMany(x => x)
                        .ToList();
                    // in case we have found a collection of simple setting values, take only the newest
                    var overridingSetting = results.FirstOrDefault(x => !(x is IConfigSection));
                    return overridingSetting != null
                        ? new[] {overridingSetting}
                        : (IEnumerable<IConfigSetting>) results;
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
        public LayeredConfigManager() : this(Enumerable.Empty<IConfiguration>()) { }

        public LayeredConfigManager Prepend(IConfigSource source)
        {
            //
            // NB prepends configs to the end, as they are being prioritized by order
            //
            source.VerifyArgument(nameof(source)).IsNotNull();
            var cfg = source.LoadConfiguration();
            if (cfg == null)
            {
                return this;
            }
            var newConfigs = _configs.ToArray().Union(new[] {cfg});
            return new LayeredConfigManager(newConfigs);
        }
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
            var newConfigs = new[] { cfg }.Union(_configs.ToArray()); 
            return new LayeredConfigManager(newConfigs);
        }

        /// <inheritdoc />
        public IConfiguration LoadConfiguration() => new LayeredConfiguration(_configs);
    }
}