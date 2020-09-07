#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal sealed class ConfigurationManagerSectionGroupAdapter : IConfigSection
    {
        private readonly IDictionary<string, IConfigSection> _sections;

        public ConfigurationManagerSectionGroupAdapter(ConfigurationSectionGroup grp, string name)
        {
            Name = name;
            _sections = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
            ConfigurationManagerSection2ConfigurationAdapter.DiscoverSections(
                Enumerable.OfType<ConfigurationSectionGroup>(grp.SectionGroups), 
                Enumerable.OfType<ConfigurationSection>(grp.Sections), 
                _sections);
        }
        public ConfigurationManagerSectionGroupAdapter(ConfigurationSectionGroup grp) : this(grp, grp.Name) { }

        public IConfigSection GetSection(string key)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
            return _sections.TryGetValue(key, out var section)
                ? section
                : null;
        }

        public IEnumerable<string> Keys => _sections.Keys;

        public IEnumerable<IConfigSetting> this[string key] => new[] { GetSection(key) };
        CharSequence IConfigSetting.Value => null;

        public string Name { get; }
    }
}
#endif