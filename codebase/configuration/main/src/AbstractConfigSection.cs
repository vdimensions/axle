using System;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Configuration
{
    public abstract class AbstractConfigSection : IConfigSection
    {
        protected IDictionary<string, IConfigSetting> Data { get; } = new Dictionary<string, IConfigSetting>(StringComparer.OrdinalIgnoreCase);

        protected AbstractConfigSection(string name)
        {
            Name = name;
        }

        public IEnumerable<string> Keys => Data.Keys;
        public string Name { get; }
        string IConfigSetting.Value => null;

        public IConfigSetting this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                return Data.TryGetValue(key, out var val) ? val : null;
            }
        }
    }
}