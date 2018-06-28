using System;
#if NETSTANDARD || NET45_OR_NEWER
#endif


namespace Axle.Application.Modularity
{
    public interface IModuleCatalog
    {
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        Type[] DiscoverModuleTypes();
        #endif

        string GetModuleName(Type moduleType);
        Type[] GetRequiredModules(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleCallback[] GetDependencyInitializedMethods(Type moduleType);
        ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType);
        ModuleMethod GetReadyMethod(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
    }
}