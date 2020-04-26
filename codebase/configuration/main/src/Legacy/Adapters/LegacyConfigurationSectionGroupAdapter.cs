#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Legacy.Adapters
{
    internal sealed class LegacyConfigurationSectionGroupAdapter : IConfigSection
    {
        private readonly IDictionary<string, IConfigSection> _sections;

        public LegacyConfigurationSectionGroupAdapter(ConfigurationSectionGroup grp, string name)
        {
            Name = name;
            _sections = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
            LegacyConfiguration2ConfigurationAdapter.DiscoverSections(
                grp.SectionGroups.OfType<ConfigurationSectionGroup>(), 
                grp.Sections.OfType<ConfigurationSection>(), 
                _sections);
        }
        public LegacyConfigurationSectionGroupAdapter(ConfigurationSectionGroup grp) : this(grp, grp.Name) { }

        public IConfigSection GetSection(string key) 
            => _sections.TryGetValue(key.VerifyArgument(nameof(key)).IsNotNullOrEmpty(), out var section) 
                ? section 
                : null;

        public IEnumerable<string> Keys => _sections.Keys;

        public IEnumerable<IConfigSetting> this[string key] => new[] { GetSection(key) };
        string IConfigSetting.Value => null;

        public string Name { get; }
    }
}
#endif