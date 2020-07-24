using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.References;

namespace Axle.Configuration
{
    internal sealed class EnvironmentConfigSource : IConfigSource
    {
        private sealed class EnvironmentConfig : IConfiguration
        {
            private readonly IDictionary<string, string> _env;

            public EnvironmentConfig(IDictionary<string, string> env)
            {
                _env = env;
            }

            IEnumerable<string> IConfigSection.Keys => _env.Keys;
            string IConfigSection.Name => string.Empty;
            string IConfigSetting.Value => null;

            IEnumerable<IConfigSetting> IConfigSection.this[string key] =>
                _env.TryGetValue(key, out var val) 
                    ? Enumerable.Repeat(ConfigSetting.Create(val), 1) 
                    : Enumerable.Empty<IConfigSetting>();
        }
        
        public static EnvironmentConfigSource Instance => Singleton<EnvironmentConfigSource>.Instance;

        private EnvironmentConfigSource() { }

        public IConfiguration LoadConfiguration()
        {
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            var envData = System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            #else
            var envData = System.Environment.GetEnvironmentVariables();
            #endif
            var env = Enumerable.ToDictionary(
                Enumerable.OfType<DictionaryEntry>(envData), 
                x => x.Key.ToString(), 
                x => x.Value.ToString(), 
                StringComparer.OrdinalIgnoreCase);
            return new EnvironmentConfig(env);
        }
    }
}