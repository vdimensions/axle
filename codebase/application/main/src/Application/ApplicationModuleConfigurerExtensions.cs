using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Application
{
    /// <summary>
    /// A static class providing extension methods for instances of the <see cref="IApplicationModuleConfigurer"/>
    /// interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationModuleConfigurerExtensions
    {
        /// <summary>
        /// Adds the specified type <typeparamref name="T" /> to the list of module types
        /// that will be initialized by an application. 
        /// </summary>
        /// <typeparam name="T">
        /// A collection of types to be included for loading during the application initialization.
        /// </typeparam>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        public static IApplicationModuleConfigurer Load<T>(this IApplicationModuleConfigurer cfg) where T: class
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cfg, nameof(cfg)));
            return Load(cfg, typeof(T));
        }

        /// <summary>
        /// Adds the specified <paramref name="type" /> to the list of module types
        /// that will be initialized by an application. 
        /// </summary>
        /// <param name="cfg">
        /// The <see cref="IApplicationModuleConfigurer"/> reference this extension method is called on.
        /// </param>
        /// <param name="type">
        /// A collection of types to be included for loading during the application initialization.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        public static IApplicationModuleConfigurer Load(this IApplicationModuleConfigurer cfg, Type type) => Load(cfg, new[]{type});
        /// <summary>
        /// Adds the specified <paramref name="types">collection of types</paramref> to the list of module types
        /// that will be initialized by an application. 
        /// </summary>
        /// <param name="cfg">
        /// The <see cref="IApplicationModuleConfigurer"/> reference this extension method is called on.
        /// </param>
        /// <param name="types">
        /// A collection of types to be included for loading during the application initialization.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        public static IApplicationModuleConfigurer Load(this IApplicationModuleConfigurer cfg, params Type[] types)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(cfg, nameof(cfg)));
            return cfg.Load(types as IEnumerable<Type>);
        }
    }
}