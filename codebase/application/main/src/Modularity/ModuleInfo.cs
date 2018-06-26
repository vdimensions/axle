using System;
using System.Collections.Generic;
using System.Reflection;

using Axle.Verification;


namespace Axle.Application.Modularity
{
    public class ModuleInfo
    {
        public ModuleInfo(Type type, string name, ModuleMethod initMethod, ModuleNotifyMethod[] onDependencyInitializedMethods, ModuleMethod onDependenciesReadyMethod, ModuleEntryMethod entryPointMethod, params ModuleInfo[] requiredModules)
        {
            Type = type.VerifyArgument(nameof(type)).IsNotAbstract();
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            InitMethod = initMethod;
            OnDependencyInitializedMethods = onDependencyInitializedMethods;
            OnDependenciesReadyMethod = onDependenciesReadyMethod;
            EntryPointMethod = entryPointMethod;
            RequiredModules = requiredModules;
        }

        public string Name { get; }
        public Type Type { get; }
        public Assembly Assembly => Type.Assembly;
        public ModuleMethod InitMethod { get; }
        public ModuleNotifyMethod[] OnDependencyInitializedMethods { get; }
        public ModuleMethod OnDependenciesReadyMethod { get; }
        public ModuleEntryMethod EntryPointMethod { get; }
        public IEnumerable<ModuleInfo> RequiredModules { get; }
    }

    

    ///// <summary>
    ///// A class representing the module shepherd; that is, a special module which is defined as depending on all modules in 
    ///// a module catalog. Its purpose is to guarantee all of those modules are initialized before the application is ran,
    ///// as well as to collect statistical information on which modules are being loaded.
    ///// </summary>
    //[Module(Name)]
    //internal sealed class ModuleShepherd : IDisposable
    //{
    //    public const string Name = "<ModuleShepherd>";
    //
    //    [ModuleInitMethod(AllowParallelInvoke = false)]
    //    public void Init() { }
    //
    //    [ModuleOnDependencyInitialized(AllowParallelInvoke = false)]
    //    public void OnDependencyInitialized(object dependency) { }
    //
    //    [ModuleOnDependenciesReady(AllowParallelInvoke = false)]
    //    public void OnDependenciesReady() { }
    //
    //    [ModuleEntryPoint(AllowParallelInvoke = false)]
    //    public void Run(params string[] args) { }
    //
    //    public void Dispose() { }
    //}
}