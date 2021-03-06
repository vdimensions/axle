#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Legacy.Adapters
{
    internal sealed class LegacyConfigSectionGroupAdapter : IConfigSection
    {
        private readonly IDictionary<string, IConfigSection> _sections;

        public LegacyConfigSectionGroupAdapter(ConfigurationSectionGroup grp, string name)
        {
            Name = name;
            _sections = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
            LegacyConfigAdapter.DiscoverSections(
                grp.SectionGroups.OfType<ConfigurationSectionGroup>(), 
                grp.Sections.OfType<ConfigurationSection>(), 
                _sections);
        }
        public LegacyConfigSectionGroupAdapter(ConfigurationSectionGroup grp) : this(grp, grp.Name) { }

        public IConfigSection GetSection(string key) => _sections.TryGetValue(key.VerifyArgument(nameof(key)).IsNotNullOrEmpty(), out var section) ? section : null;

        public IEnumerable<string> Keys => _sections.Keys;

        public IConfigSetting this[string key] => GetSection(key);
        string IConfigSetting.Value => null;

        public string Name { get; }
    }
}
#endif