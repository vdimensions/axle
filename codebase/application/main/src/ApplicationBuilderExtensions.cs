using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        public static IApplicationBuilder AddConfig(
            this IApplicationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.ConfigureApplication(a => a.Append(configurationSource));
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IApplicationBuilder EnableLegacyConfig(this IApplicationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.ConfigureApplication(a => a.AppendLegacyConfig());
        }
        #endif
    }
}