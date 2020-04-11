using System;
using System.Linq;
using System.Reflection;
using Axle.Configuration;


namespace Axle.Modularity
{
    internal sealed class ModuleCatalogWrapper : IModuleCatalog
    {
        private readonly IModuleCatalog _originalCatalog;
        private readonly Type[] _parentModuleTypes =
        {
            typeof(StatisticsModule),
            //#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            //typeof(DynamicModuleLoader)
            //#endif
            typeof(ConfigurationModule)
        };

        public ModuleCatalogWrapper(IModuleCatalog originalCatalog)
        {
            _originalCatalog = originalCatalog;
        }

        public Type[] DiscoverModuleTypes(Assembly assembly) => _originalCatalog.DiscoverModuleTypes(assembly);

        public Type GetRequiredApplicationHostType(Type moduleType)
        {
            return _originalCatalog.GetRequiredApplicationHostType(moduleType);
        }

        public Type[] GetRequiredModules(Type moduleType)
        {
            var result = _originalCatalog.GetRequiredModules(moduleType);
            if (_parentModuleTypes.All(t => t != moduleType))
            {
                result = result.Union(_parentModuleTypes).ToArray();
            }
            return result;
        }

        public ModuleMethod GetInitMethod(Type moduleType) => _originalCatalog.GetInitMethod(moduleType);

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType) => _originalCatalog.GetEntryPointMethod(moduleType);

        public ModuleMethod GetTerminateMethod(Type moduleType) => _originalCatalog.GetTerminateMethod(moduleType);

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType) => _originalCatalog.GetDependencyInitializedMethods(moduleType);

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType) => _originalCatalog.GetDependencyTerminatedMethods(moduleType);

        public UtilizesAttribute[] GetUtilizedModules(Type moduleType) => _originalCatalog.GetUtilizedModules(moduleType);
        public ReportsToAttribute[] GetReportsToModules(Type moduleType) => _originalCatalog.GetReportsToModules(moduleType);
        public ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType) => _originalCatalog.GetCommandLineTrigger(moduleType);
        public ModuleConfigSectionAttribute GetConfigurationInfo(Type moduleType) => _originalCatalog.GetConfigurationInfo(moduleType);
    }
}