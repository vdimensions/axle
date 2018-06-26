using System;
#if NETSTANDARD || NET45_OR_NEWER
#endif


namespace Axle.Application.Modularity
{
    public interface IReflectionProvider
    {
        Type[] DiscoverModuleTypes();
        string GetModuleName(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleNotifyMethod[] GetDependencyInitializedMethods(Type moduleType);
        ModuleMethod GetDependenciesReadyMethodMethod(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
        Type[] GetRequiredModules(Type moduleType);
    }
}