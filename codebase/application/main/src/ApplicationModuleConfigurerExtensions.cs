using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationModuleConfigurerExtensions
    {
        public static IApplicationModuleConfigurer Load<T>(this IApplicationModuleConfigurer cfg) where T: class
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cfg, nameof(cfg)));
            return Load(cfg, typeof(T));
        }
        public static IApplicationModuleConfigurer Load(this IApplicationModuleConfigurer cfg, Type type) => Load(cfg, new[]{type});
        public static IApplicationModuleConfigurer Load(this IApplicationModuleConfigurer cfg, params Type[] types)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cfg, nameof(cfg)));
            return cfg.Load(types as IEnumerable<Type>);
        }
    }
}