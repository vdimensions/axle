using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Microsoft;
#endif
using Axle.Verification;

namespace Axle
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationConfigurationBuilderExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IApplicationConfigurationBuilder EnableLegacyConfig(this IApplicationConfigurationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Add(new Axle.Configuration.Legacy.LegacyConfigSource());
        }
        #endif
        
        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        public static IApplicationConfigurationBuilder Add(
            this IApplicationConfigurationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Add(new FileConfigSource(configurationSource));
        }
        #endif
    }
}