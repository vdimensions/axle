using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Legacy.Adapters
{
    internal abstract class AbstractLegacyConfig2ConfigSectionAdapter : IConfigSection
    {
        protected AbstractLegacyConfig2ConfigSectionAdapter(string name)
        {
            Name = name;
        }

        public IConfigSection GetSection(string key) => Sections.TryGetValue(key.VerifyArgument(nameof(key)).IsNotNullOrEmpty(), out var section) ? section : null;

        protected IDictionary<string, IConfigSection> Sections { get; } = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
        protected IDictionary<string, IConfigSetting> Properties { get; } = new Dictionary<string, IConfigSetting>(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<string> Keys => Sections.Keys.Union(Properties.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                return Sections.TryGetValue(key, out var section) 
                    ? new IConfigSetting[] { section }
                    : Properties.TryGetValue(key, out var result) 
                        ? new []{ result }
                        : Enumerable.Empty<IConfigSetting>();
            }
        }

        public string Name { get; }
        string IConfigSetting.Value => null;
    }
}