using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Configuration.Legacy;
#endif
#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Microsoft;
#endif
using Axle.Verification;

namespace Axle
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationConfigurationBuilderExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        public static IApplicationConfigurationBuilder Prepend(
            this IApplicationConfigurationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Prepend(new FileConfigSource(configurationSource));
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IApplicationConfigurationBuilder PrependLegacyConfig(
            this IApplicationConfigurationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Prepend(new LegacyConfigSource());
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        public static IApplicationConfigurationBuilder Append(
            this IApplicationConfigurationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Append(new FileConfigSource(configurationSource));
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        internal static IApplicationConfigurationBuilder AppendLegacyConfig(this IApplicationConfigurationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Append(new LegacyConfigSource());
        }
        #endif
    }
}