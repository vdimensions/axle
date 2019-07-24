using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.References;

namespace Axle.Configuration
{
    internal sealed class EnvironmentConfigSource : IConfigSource, IConfiguration
    {
        public static EnvironmentConfigSource Instance => Singleton<EnvironmentConfigSource>.Instance;

        private readonly IDictionary<string, string> _env;

        private EnvironmentConfigSource()
        {
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            var env = System.Environment.GetEnvironmentVariables(System.EnvironmentVariableTarget.Process);
            #else
            var env = System.Environment.GetEnvironmentVariables();
            #endif
            _env = Enumerable.ToDictionary(
                Enumerable.OfType<DictionaryEntry>(env), 
                x => x.Key.ToString(), 
                x => x.Value.ToString(), 
                StringComparer.OrdinalIgnoreCase);
        }

        public IConfiguration LoadConfiguration() => this;

        IConfigSetting IConfigSection.this[string key] => _env.TryGetValue(key, out var val) ? ConfigSetting.Create(val) : null;

        IEnumerable<string> IConfigSection.Keys => _env.Keys;

        public string Name => string.Empty;
        public string Value => null;
    }
}