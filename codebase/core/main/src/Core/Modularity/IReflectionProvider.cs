using System;

using Axle.Reflection;


namespace Axle.Core.Modularity
{
    public interface IReflectionProvider
    {
        ModuleInfo[] GetModuleDefinitions();
        string GetModuleName(Type moduleType);
        IInvokable GetInitMethod(Type moduleType);
        IInvokable GetDependencyInitializedMethod(Type moduleType);
        IInvokable GetDependenciesReadyMethodMethod(Type moduleType);
        IInvokable GetEntryPointMethod(Type moduleType);
        Type[] GetRequiredModules(Type moduleType);
    }
}