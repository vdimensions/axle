#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Linq;
using Axle.Text.Data.Binding;
using Axle.Verification;

namespace Axle.Configuration
{
    /// <summary>
    /// A static class containing common configuration extension methods.
    /// </summary>
    public static class ConfigSectionExtensions
    {
        private sealed class ConfigurationBindingValueProvider : IBoundComplexValueProvider
        {
            private readonly IConfigSection _config;

            public ConfigurationBindingValueProvider(IConfigSection config)
            {
                _config = config;
            }

            public bool TryGetValue(string member, out IBoundValueProvider value)
            {
                value = this[member];
                return value != null;
            }

            public IBoundValueProvider this[string member]
            {
                get
                {
                    var setting = _config[member];
                    if (setting == null)
                    {
                        var cmp = StringComparer.OrdinalIgnoreCase;
                        setting = _config.Keys.Where(x => cmp.Equals(x, member)).Select(x => _config[x]).SingleOrDefault();
                    }
                    switch (setting)
                    {
                        case null:
                            return null;
                        case IConfigSection cs:
                            return new ConfigurationBindingValueProvider(cs);
                        default:
                            return new BoundSimpleValueProvider(member, setting.Value);
                    }
                }
            }
        
            public string Name => _config.Name;
        }
        
        public static IConfigSection GetSection(this IConfigSection config, string sectionName)
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            sectionName.VerifyArgument(nameof(sectionName)).IsNotNullOrEmpty();
            return config[sectionName] as IConfigSection;
        }

        public static object GetSection(this IConfigSection config, string sectionName, Type sectionType)
        {
            var configSection = GetSection(config, sectionName);
            return configSection != null 
                ? new DefaultBinder().Bind(
                    new ConfigurationBindingValueProvider(configSection), 
                    sectionType)
                : null;
        }

        public static T GetSection<T>(this IConfigSection config, string sectionName) where T: class, new()
        {
            var configSection = GetSection(config, sectionName);
            return configSection != null
                ? (T) new DefaultBinder().Bind(
                    new ConfigurationBindingValueProvider(configSection), 
                    new T())
                : null;
        }
    }
}
#endif
