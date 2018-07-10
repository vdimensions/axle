using System;
#if NETSTANDARD || NET45_OR_NEWER
#endif


namespace Axle.Modularity
{
    public interface IModuleCatalog
    {
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        Type[] DiscoverModuleTypes();
        #endif

        Type[] GetRequiredModules(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleCallback[] GetDependencyInitializedMethods(Type moduleType);
        ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType);
        ModuleMethod GetTerminateMethod(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
    }
}