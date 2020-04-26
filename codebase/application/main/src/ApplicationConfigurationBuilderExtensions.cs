using System;
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
        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        [Obsolete]
        public static IApplicationConfigurationBuilder Prepend(
            this IApplicationConfigurationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Prepend(new FileConfigSource(configurationSource));
        }
        #endif


        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        [Obsolete]
        public static IApplicationConfigurationBuilder Append(
            this IApplicationConfigurationBuilder builder, 
            Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Append(new FileConfigSource(configurationSource));
        }
        #endif
    }
}