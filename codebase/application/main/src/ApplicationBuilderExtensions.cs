using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationBuilderExtensions
    {
        [Obsolete("Use `ConfigureModules` instead")] 
        public static IApplicationBuilder Load<T>(this IApplicationBuilder builder) where T: class
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return Load(builder, typeof(T));
        }
        [Obsolete("Use `ConfigureModules` instead")] 
        public static IApplicationBuilder Load(this IApplicationBuilder builder, Type type) => Load(builder, new[]{type});
        [Obsolete("Use `ConfigureModules` instead")] 
        public static IApplicationBuilder Load(this IApplicationBuilder builder, params Type[] types)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Load(types as IEnumerable<Type>);
        }

        #if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
        public static IApplicationBuilder AddConfig(this IApplicationBuilder builder, Microsoft.Extensions.Configuration.FileConfigurationSource configurationSource)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.ConfigureApplication(a => a.Append(configurationSource));
        }
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [Obsolete("Use EnableLegacyConfig instead")]
        public static IApplicationBuilder AddLegacyConfig(this IApplicationBuilder builder) => EnableLegacyConfig(builder);
        public static IApplicationBuilder EnableLegacyConfig(this IApplicationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.ConfigureApplication(a => a.AppendLegacyConfig());
        }
        #endif
    }
}