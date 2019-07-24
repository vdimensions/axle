#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Axle.Reflection.Extensions.Type;
using Axle.Verification;

namespace Axle.Configuration.Legacy.Adapters
{
    internal abstract class AbstractLegacySection : IConfigSection
    {
        protected AbstractLegacySection(string name)
        {
            Name = name;
        }

        protected IDictionary<string, IConfigSection> Sections { get; } = new Dictionary<string, IConfigSection>(StringComparer.OrdinalIgnoreCase);
        protected IDictionary<string, IConfigSetting> Properties { get; } = new Dictionary<string, IConfigSetting>(StringComparer.OrdinalIgnoreCase);

        public IConfigSection GetSection(string key) => Sections.TryGetValue(key.VerifyArgument(nameof(key)).IsNotNullOrEmpty(), out var section) ? section : null;

        public IEnumerable<string> Keys => Sections.Keys.Union(Properties.Keys).Distinct(StringComparer.OrdinalIgnoreCase);

        public IConfigSetting this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNullOrEmpty();
                return Sections.TryGetValue(key, out var section) 
                    ? section
                    : Properties.TryGetValue(key, out var result) 
                        ? result 
                        : null;
            }
        }

        public string Name { get; }
        string IConfigSetting.Value => null;
    }

    internal sealed class LegacyConfigElementAdapter : AbstractLegacySection
    {
        public LegacyConfigElementAdapter(ConfigurationElement section, string name) : base(name)
        {
            //var configProperties = GetProperties(section);
            foreach (PropertyInformation propertyInfo in section.ElementInformation.Properties)
            {
                var propertyName = propertyInfo.Name;
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                var value = propertyInfo.Value;
                if (value == null)
                {
                    continue;
                }
                if (propertyInfo.Converter != null)
                {
                    string strValue = null;
                    try
                    {
                        strValue = propertyInfo.Converter.ConvertToString(value);
                    }
                    catch
                    {
                        if (value.GetType().GetTypeCode() != TypeCode.Object)
                        {
                            strValue = value.ToString();
                        }
                    }
                    if (strValue != null)
                    {
                        Properties.Add(propertyName, ConfigSetting.Create(strValue));
                    }                    
                }
                else if (value is ConfigurationElement e)
                {
                    var s = LegacyConfigAdapter.MapElement(e, propertyName);
                    if (s != null)
                    {
                        Sections.Add(propertyName, s);
                    }
                }
            }
        }
    }
}
#endif