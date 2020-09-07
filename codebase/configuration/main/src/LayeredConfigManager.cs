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
                    StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
                    var results = Enumerable.ToList(
                        Enumerable.SelectMany(
                            _configurations, 
                            x => Enumerable.Where(x[key], y => y is IConfigSection)));
                    if (results.Count == 0)
                    {
                        results = Enumerable.ToList(
                            Enumerable.FirstOrDefault(
                                Enumerable.Select(
                                    _configurations, 
                                    x => Enumerable.ToArray(x[key])), 
                                x => x.Length > 0 && Enumerable.All(x, y => !(y is IConfigSection))) ?? new IConfigSetting[0]);
                    }
                    return results;
                }
            }

            public IEnumerable<string> Keys => Enumerable.Distinct(
                Enumerable.SelectMany(_configurations, c => c.Keys), StringComparer.OrdinalIgnoreCase);

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
            Verifier.IsNotNull(Verifier.VerifyArgument(source, nameof(source)));
            var cfg = source.LoadConfiguration();
            if (cfg == null)
            {
                return this;
            }
            return new LayeredConfigManager(Enumerable.ToArray(Enumerable.Union(new[] { cfg }, _configs)));
        }

        public LayeredConfigManager Append(IConfiguration config)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(config, nameof(config)));
            return new LayeredConfigManager(Enumerable.ToArray(Enumerable.Union(new[] { config }, _configs)));
        }

        /// <inheritdoc />
        public IConfiguration LoadConfiguration() => _configs.Length == 0 ? null : new LayeredConfiguration(_configs);
    }
}