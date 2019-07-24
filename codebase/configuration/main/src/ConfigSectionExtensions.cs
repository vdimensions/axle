#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Verification;
#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Adapters;
using Microsoft.Extensions.Configuration;
#endif

namespace Axle.Configuration
{
    /// <summary>
    /// A static class containing common configuration extension methods.
    /// </summary>
    public static class ConfigSectionExtensions
    {
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
            #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
            var msConfig = new ConfigurationRoot(new IConfigurationProvider[]{new AxleConfigurationProvider(config.GetSection(sectionName))});
            var result = new T();
            msConfig.Bind(result);
            return result;
            #else
            return null;
            #endif
        }
    }
}
#endif
