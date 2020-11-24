using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Axle.Logging;
using Axle.Modularity;

namespace Axle.Application
{
    internal sealed class ApplicationModuleCatalog : IModuleCatalog
    {
        private readonly IModuleCatalog _originalCatalog;
        private readonly HashSet<Type> _applicationModuleTypes = new HashSet<Type>
            {
                typeof(StatisticsModule),
                //#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                //typeof(DynamicModuleLoader)
                //#endif
                typeof(LoggingModule),          // guarantees the log messages during app initialization will be flushed 
            };

        public ApplicationModuleCatalog(IModuleCatalog originalCatalog)
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
            return _applicationModuleTypes.Contains(moduleType)
                ? _originalCatalog.GetRequiredModules(moduleType)
                : _applicationModuleTypes.Union(_originalCatalog.GetRequiredModules(moduleType)).ToArray();
        }

        public ModuleMethod GetInitMethod(Type moduleType) => _originalCatalog.GetInitMethod(moduleType);
        
        public ModuleMethod GetReadyMethod(Type moduleType) => _originalCatalog.GetReadyMethod(moduleType);

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType) => _originalCatalog.GetEntryPointMethod(moduleType);

        public ModuleMethod GetTerminateMethod(Type moduleType) => _originalCatalog.GetTerminateMethod(moduleType);

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType) => _originalCatalog.GetDependencyInitializedMethods(moduleType);

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType) => _originalCatalog.GetDependencyTerminatedMethods(moduleType);

        public UtilizesAttribute[] GetUtilizedModules(Type moduleType) => _originalCatalog.GetUtilizedModules(moduleType);
        public ProvidesForAttribute[] GetProvidesForModules(Type moduleType) => _originalCatalog.GetProvidesForModules(moduleType);
        public ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType) => _originalCatalog.GetCommandLineTrigger(moduleType);
        public ModuleConfigSectionAttribute GetConfigurationInfo(Type moduleType) => _originalCatalog.GetConfigurationInfo(moduleType);

        internal IEnumerable<Type> ApplicationModuleTypes => _applicationModuleTypes;
    }
}