using System;
using System.Reflection;


namespace Axle.Modularity
{
    /// <summary>
    /// An interface representing a an application module catalog. The module catalog is responsible for discovering
    /// and supplying runtime information for instantiating and executing the application modules.
    /// </summary>
    internal interface IModuleCatalog
    {
        /// <summary>
        /// Examines the types within the provided <paramref name="assembly"/> and returns those representing
        /// application modules.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to examine.
        /// </param>
        /// <returns>
        /// An array of <see cref="Type"/> each representing an application module.
        /// </returns>
        Type[] DiscoverModuleTypes(Assembly assembly);

        Type GetRequiredApplicationHostType(Type moduleType);
        
        /// <summary>
        /// Gets an array of <see cref="Type"/> each representing a module that must be initialized before
        /// the module represented by the provided <paramref name="moduleType"/>.
        /// </summary>
        /// <param name="moduleType">
        /// The type of the module to get the required modules for.
        /// </param>
        /// <returns>
        /// An array of <see cref="Type"/> each representing a module that must be initialized before
        /// the module represented by the provided <paramref name="moduleType"/>.
        /// </returns>
        Type[] GetRequiredModules(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleMethod GetReadyMethod(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
        ModuleMethod GetTerminateMethod(Type moduleType);
        ModuleCallback[] GetDependencyInitializedMethods(Type moduleType);
        ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType);
        UtilizesAttribute[] GetUtilizedModules(Type moduleType);
        ReportsToAttribute[] GetReportsToModules(Type moduleType);
        ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType);
        ModuleConfigSectionAttribute GetConfigurationInfo(Type moduleType);
    }
}