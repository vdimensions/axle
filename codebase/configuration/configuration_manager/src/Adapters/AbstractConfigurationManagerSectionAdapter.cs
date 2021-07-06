using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal abstract class AbstractConfigurationManagerSectionAdapter : IConfigSection
    {
        protected AbstractConfigurationManagerSectionAdapter(string name)
        {
            Name = name;
        }

        public IConfigSection GetSection(string key)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
            return Sections.TryGetValue(key, out var section)
                ? section
                : null;
        }

        protected IDictionary<string, IConfigSection> Sections { get; } = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
        protected IDictionary<string, IConfigSetting> Properties { get; } = new Dictionary<string, IConfigSetting>(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<string> Keys => Enumerable.Distinct(Enumerable.Union(Sections.Keys, Properties.Keys), StringComparer.OrdinalIgnoreCase);

        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
                return Sections.TryGetValue(key, out var section) 
                    ? new IConfigSetting[] { section }
                    : Properties.TryGetValue(key, out var result) 
                        ? new []{ result }
                        : Enumerable.Empty<IConfigSetting>();
            }
        }

        public string Name { get; }
        CharSequence IConfigSetting.Value => null;
    }
}