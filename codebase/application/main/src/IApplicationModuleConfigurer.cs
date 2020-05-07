using System;
using System.Collections.Generic;
using System.Reflection;

namespace Axle
{
    /// <summary>
    /// An interface that enables specifying which assemblies to be scanned for modules and which module types to be
    /// loaded by the currently configured axle application.
    /// </summary>
    public interface IApplicationModuleConfigurer
    {
        /// <summary>
        /// Adds the specified <paramref name="types">collection of types</paramref> to the list of module types
        /// that will be initialized by an application. 
        /// </summary>
        /// <param name="types">
        /// A collection of types to be included for loading during the application initialization.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        IApplicationModuleConfigurer Load(IEnumerable<Type> types);
        /// <summary>
        /// Scans the specified <paramref name="assembly"/> and adds any discovered module types to the list of module
        /// types that will be initialized by an application. 
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to scan for module types.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        IApplicationModuleConfigurer Load(Assembly assembly);
        /// <summary>
        /// Scans the specified collection of <paramref name="assemblies"/> and adds any discovered module types to the
        /// list of module types that will be initialized by an application. 
        /// </summary>
        /// <param name="assemblies">
        /// A collection of <see cref="Assembly"/> instances, each be to scan for module types.
        /// </param>
        /// <returns>
        /// A reference to the current <see cref="IApplicationModuleConfigurer"/> instance.
        /// </returns>
        IApplicationModuleConfigurer Load(IEnumerable<Assembly> assemblies);
    }
}