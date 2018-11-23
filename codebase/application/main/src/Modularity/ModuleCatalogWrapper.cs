using System;
using System.Linq;
using System.Reflection;


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
        };

        public ModuleCatalogWrapper(IModuleCatalog originalCatalog)
        {
            _originalCatalog = originalCatalog;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public Type[] DiscoverModuleTypes() => _originalCatalog.DiscoverModuleTypes();
        #endif

        public Type[] DiscoverModuleTypes(Assembly assembly) => _originalCatalog.DiscoverModuleTypes(assembly);

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

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType) => _originalCatalog.GetDependencyInitializedMethods(moduleType);

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType) => _originalCatalog.GetDependencyTerminatedMethods(moduleType);

        public ModuleMethod GetTerminateMethod(Type moduleType) => _originalCatalog.GetTerminateMethod(moduleType);

        public UtilizesAttribute[] GetUtilizedModules(Type moduleType) => _originalCatalog.GetUtilizedModules(moduleType);
        public UtilizedByAttribute[] GetUtilizedByModules(Type moduleType) => _originalCatalog.GetUtilizedByModules(moduleType);

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType) => _originalCatalog.GetEntryPointMethod(moduleType);
    }
}