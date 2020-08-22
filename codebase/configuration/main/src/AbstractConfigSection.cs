using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration
{
    public abstract class AbstractConfigSection : IConfigSection
    {
        protected IDictionary<string, IList<IConfigSetting>> Data { get; } = new Dictionary<string, IList<IConfigSetting>>(StringComparer.OrdinalIgnoreCase);

        protected AbstractConfigSection(string name)
        {
            Name = name;
        }

        public IEnumerable<string> Keys => Data.Keys;
        public string Name { get; }
        CharSequence IConfigSetting.Value => null;

        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                return Data.TryGetValue(key, out var val) ? val : Enumerable.Empty<IConfigSetting>();
            }
        }
    }
}