using System;
using System.Reflection;


namespace Axle.Modularity
{
    public interface IModuleCatalog
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        Type[] DiscoverModuleTypes();
        #endif

        Type[] DiscoverModuleTypes(Assembly assembly);

        Type[] GetRequiredModules(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleCallback[] GetDependencyInitializedMethods(Type moduleType);
        ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType);
        ModuleMethod GetTerminateMethod(Type moduleType);
        UtilizesAttribute[] GetUtilizedModules(Type moduleType);
        UtilizedByAttribute[] GetUtilizedByModules(Type moduleType);
        ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
    }
}