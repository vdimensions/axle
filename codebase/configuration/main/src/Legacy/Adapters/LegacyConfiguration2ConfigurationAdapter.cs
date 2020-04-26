#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Axle.Verification;

namespace Axle.Configuration.Legacy.Adapters
{
    internal sealed class LegacyConfiguration2ConfigurationAdapter : IConfiguration
    {
        internal static IConfigSection MapElement(ConfigurationElement e, string name)
        {
            switch (e)
            {
                case AppSettingsSection appSettings:
                    return new AppSettings2ConfigSectionAdapter(appSettings, name);
                case ConnectionStringsSection connectionStrings:
                    return new ConnectionStringsSectionAdapter(connectionStrings);
                case NameValueConfigurationCollection nameValueConfigurationCollection:
                    return new LegacyNameValueSectionAdapter(nameValueConfigurationCollection, name);
                case ConfigurationElement element:
                    return new LegacyConfigurationElement2ConfigSectionAdapter(element, name);
                default:
                    return null;
            }
        }

        internal static void DiscoverSections(
            IEnumerable<ConfigurationSectionGroup> sectionGroups,
            IEnumerable<ConfigurationSection> sections, 
            IDictionary<string, IConfigSection> adaptedSections)
        {
            foreach (var g in sectionGroups)
            {
                var section = new LegacyConfigurationSectionGroupAdapter(g);
                adaptedSections.Add(section.Name, section);
            }

            foreach (var s in sections)
            {
                var section = MapElement(s, s.SectionInformation.SectionName);
                if (section == null)
                {
                    continue;
                }
                adaptedSections.Add(section.Name, section);
            }
        }

        private readonly IDictionary<string, IConfigSection> _sections;
        private readonly AppSettings2ConfigSectionAdapter _appSettings;

        public LegacyConfiguration2ConfigurationAdapter(System.Configuration.Configuration cfg)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cfg, nameof(cfg)));
            _sections = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);

            _appSettings = new AppSettings2ConfigSectionAdapter(cfg.AppSettings, nameof(cfg.AppSettings));
            var connectionStringsSection = new ConnectionStringsSectionAdapter(cfg.ConnectionStrings);
            _sections.Add(connectionStringsSection.Name, connectionStringsSection);
            var cmp = StringComparer.OrdinalIgnoreCase;
            DiscoverSections(
                cfg.SectionGroups.OfType<ConfigurationSectionGroup>(), 
                cfg.Sections
                    .OfType<ConfigurationSection>()
                    .Where(s => !cmp.Equals(s.SectionInformation.SectionName, _appSettings.Name))
                    .Where(s => !cmp.Equals(s.SectionInformation.SectionName, connectionStringsSection.Name)), 
                _sections);
        }

        public IConfigSection GetSection(string key)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
            return _sections.TryGetValue(key, out var section) ? section : null;
        }

        public IEnumerable<string> Keys => _sections.Keys.Union(_appSettings.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                var result = GetSection(key);
                return result == null
                    ? _appSettings[key]
                    : new IConfigSetting[] {result};
            }
        }

        string IConfigSection.Name => string.Empty;
        string IConfigSetting.Value => null;
    }
}
#endif