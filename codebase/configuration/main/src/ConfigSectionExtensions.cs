
using System.Linq;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Conversion.Binding;
using Axle.Verification;
//#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
//using Axle.Configuration.Adapters;
//using Microsoft.Extensions.Configuration;
//#endif

namespace Axle.Configuration
{
    /// <summary>
    /// A static class containing common configuration extension methods.
    /// </summary>
    public static class ConfigSectionExtensions
    {
        internal sealed class ConfigurationBindingValueProvider : IComplexMemberValueProvider
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

        public static T GetSection<T>(this IConfigSection config, string sectionName) where T: class, new()
        {
            config.VerifyArgument(nameof(config)).IsNotNull();
            sectionName.VerifyArgument(nameof(sectionName)).IsNotNullOrEmpty();
            var result = new T();
            //#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
            //var msConfig = new ConfigurationRoot(new IConfigurationProvider[]{new AxleConfigurationProvider(config.GetSection(sectionName))});
            //msConfig.Bind(result);
            //return result;
            //#else
            //return null;
            //#endif
            var binder = new DefaultBinder();
            return (T) binder.Bind(new ConfigurationBindingValueProvider(config.GetSection(sectionName)), result);
        }
    }
}
#endif
