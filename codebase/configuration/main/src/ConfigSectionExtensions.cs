#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Linq;
using Axle.Conversion.Binding;
using Axle.Verification;

namespace Axle.Configuration
{
    /// <summary>
    /// A static class containing common configuration extension methods.
    /// </summary>
    public static class ConfigSectionExtensions
    {
        private sealed class ConfigurationBindingValueProvider : IComplexMemberValueProvider
        {
            private readonly IConfigSection _config;

            public ConfigurationBindingValueProvider(IConfigSection config)
            {
                _config = config;
            }

            public bool TryGetValue(string member, out IBindingValueProvider value)
            {
                value = this[member];
                return value != null;
            }

            public IBindingValueProvider this[string member]
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
                            return new SimpleMemberValueProvider(member, setting.Value);
                    }
                }
            }
        
            public string Name => _config.Name;
        }
        
        public static IConfigSection GetSection(this IConfigSection config, string sectionName)
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            sectionName.VerifyArgument(nameof(sectionName)).IsNotNullOrEmpty();
            if (config[sectionName] is IConfigSection result)
            {
                return result;
            }
            return null;
        }

        public static object GetSection(this IConfigSection config, string sectionName, Type sectionType)
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            sectionName.VerifyArgument(nameof(sectionName)).IsNotNullOrEmpty();
            return new DefaultBinder().Bind(
                new ConfigurationBindingValueProvider(config.GetSection(sectionName)), 
                sectionType);
        }

        public static T GetSection<T>(this IConfigSection config, string sectionName) where T: class, new()
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            sectionName.VerifyArgument(nameof(sectionName)).IsNotNullOrEmpty();
            return (T) new DefaultBinder().Bind(
                new ConfigurationBindingValueProvider(config.GetSection(sectionName)), 
                new T());
        }
    }
}
#endif
