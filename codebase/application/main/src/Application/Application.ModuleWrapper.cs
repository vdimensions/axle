using System;
using System.Collections.Generic;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
using System.Threading.Tasks;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Threading.Extensions.Tasks;

namespace Axle.Application
{
    partial class Application
    {
        private sealed class ModuleWrapper
        {
            private ModuleWrapper(
                ModuleInfo moduleInfo, 
                object instance, 
                IDependencyContainer dependencyContainer, 
                IDependencyExporter exporter, 
                ILogger logger,
                string[] args)
            {
                ModuleInfo = moduleInfo;
                ModuleInstance = instance;
                DependencyContainer = dependencyContainer;
                Exporter = exporter;
                Logger = logger;
                Args = args;
            }

            public ModuleWrapper(ModuleInfo moduleInfo) : this(moduleInfo, null, null, null, null, new string[0]) { }
            private ModuleWrapper(
                    ModuleInfo moduleInfo, 
                    object instance, 
                    IDependencyContainer dependencyContainer, 
                    IDependencyExporter exporter, 
                    ILogger logger, 
                    string[] args, 
                    ModuleStates state) 
                : this(moduleInfo, instance, dependencyContainer, exporter, logger, args)
            {
                State = state;
            }

            public ModuleWrapper UpdateInstance(
                object moduleInstance, 
                IDependencyContainer dependencyContainer, 
                IDependencyExporter exporter, 
                ILogger logger,
                string[] args) => new ModuleWrapper(ModuleInfo, moduleInstance, dependencyContainer, exporter, logger, args, State|ModuleStates.Instantiated);

            private ModuleWrapper ChangeState(ModuleStates state) => new ModuleWrapper(ModuleInfo, ModuleInstance, DependencyContainer, Exporter, Logger, Args, state);

            private void Notify(IList<ModuleInfo> requiredModules, IDictionary<Type, ModuleWrapper> moduleMetadata, Func<ModuleInfo, ModuleCallback[]> callbackProvider)
            {
                if (requiredModules.Count <= 0)
                {
                    return;
                }
                for (var k = 0; k < requiredModules.Count; k++)
                {
                    var rm = requiredModules[k];
                    if (!moduleMetadata.TryGetValue(rm.Type, out var targetContext))
                    {
                        continue;
                    }

                    var callbacks = callbackProvider(targetContext.ModuleInfo);
                    var parallelCallbacks = new List<Task>(callbacks.Length);
                    var sequentialCallbacks = new List<Task>(callbacks.Length);
                    for (var l = 0; l < callbacks.Length; l++)
                    {
                        var callback = callbacks[l];
                        #if NETSTANDARD || NET45_OR_NEWER
                        if (!callback.ArgumentType.GetTypeInfo().IsInstanceOfType(ModuleInstance))
                        #else
                        if (!callback.ArgumentType.IsInstanceOfType(ModuleInstance))
                        #endif
                        {
                            continue;
                        }

                        var listToAddTaskTo = callback.AllowParallelInvoke ? parallelCallbacks : sequentialCallbacks;
                        listToAddTaskTo.Add(new Task(() => callback.Invoke(targetContext.ModuleInstance, ModuleInstance)));
                    }

                    parallelCallbacks.Start(TaskScheduler.Default);
                    sequentialCallbacks.RunSynchronously();
                    parallelCallbacks.WaitForAll();
                    sequentialCallbacks.WaitForAll();
                }
            }

            public ModuleWrapper Init(IDictionary<Type, ModuleWrapper> moduleMetadata)
            {
                var exporter = Exporter;
                var requiredModules = ModuleInfo.RequiredModules;
                var args = Args;
                if ((State & ModuleStates.Terminated) == ModuleStates.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it has been terminated.");
                }
                if ((State & ModuleStates.Instantiated) != ModuleStates.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it has not been instantiated.");
                }
                if ((State & ModuleStates.Initialized) == ModuleStates.Initialized)
                {
                    Logger.Warn("Initialization attempted, but module `{0}` was already initialized. ", ModuleInfo.Type.FullName);
                    return this;
                }

                //Logger.Debug("Initializing module `{0}`...", ModuleInfo.Type.FullName);

                ModuleInfo.InitMethod?.Invoke(ModuleInstance, exporter, args);
                var result = ChangeState(State | ModuleStates.Initialized);
                Notify(requiredModules, moduleMetadata, m => m.DependencyInitializedMethods);

                Logger.Write(LogSeverity.Info, "Module `{0}` initialized.", ModuleInfo.Type.FullName);

                return result;
            }
            
            public ModuleWrapper Prepare()
            {
                if ((State & ModuleStates.Terminated) == ModuleStates.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed since it has been terminated.");
                }
                if ((State & ModuleStates.Instantiated) != ModuleStates.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be instantiated first. ");
                }
                if ((State & ModuleStates.Initialized) != ModuleStates.Initialized)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be initialized first.");
                }
                if ((State & ModuleStates.Prepared) == ModuleStates.Prepared)
                {   //
                    // Module is already ran
                    //
                    Logger.Warn("Preparation attempted, but module `{0}` was already prepared. ", ModuleInfo.Type.FullName);
                    return this;
                }
                ModuleInfo.ReadyMethod?.Invoke(ModuleInstance, Exporter, Args);
                return ChangeState(State | ModuleStates.Prepared);
            }
            
            public ModuleWrapper Run()
            {
                if ((State & ModuleStates.Terminated) == ModuleStates.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed since it has been terminated.");
                }
                if ((State & ModuleStates.Instantiated) != ModuleStates.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be instantiated first. ");
                }
                if ((State & ModuleStates.Initialized) != ModuleStates.Initialized)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be initialized first.");
                }
                if ((State & ModuleStates.Prepared) != ModuleStates.Prepared)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be prepared first.");
                }
                if ((State & ModuleStates.Ran) == ModuleStates.Ran)
                {   //
                    // Module is already ran
                    //
                    Logger.Warn("Execution attempted, but module `{0}` was already executed. ", ModuleInfo.Type.FullName);
                    return this;
                }
                ModuleInfo.EntryPointMethod?.Invoke(ModuleInstance, Args);
                return ChangeState(State | ModuleStates.Ran);
            }

            public ModuleWrapper Terminate(IDictionary<Type, ModuleWrapper> moduleMetadata)
            {
                var exporter = Exporter;
                var requiredModules = ModuleInfo.RequiredModules;
                var args = Args;
                if ((State & ModuleStates.Terminated) == ModuleStates.Terminated)
                {   //
                    // Module is already terminated
                    //
                    Logger.Warn("Termination attempted, but module `{0}` was already terminated. ", ModuleInfo.Type.FullName);
                    return this;
                }

                //Logger.Debug("Terminating module `{0}` ...", ModuleInfo.Type.FullName);

                Notify(requiredModules, moduleMetadata, m => m.DependencyTerminatedMethods);

                ModuleInfo.TerminateMethod?.Invoke(ModuleInstance, exporter, args);
                
                if (ModuleInstance is IDisposable d)
                {
                    d.Dispose();
                }
                
                var result = ChangeState(State | ModuleStates.Terminated);

                Logger.Write(LogSeverity.Info, "Module `{0}` terminated. ", ModuleInfo.Type.FullName);

                return result;
            }

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public IDependencyContainer DependencyContainer { get; }
            public IDependencyExporter Exporter { get; }
            public ILogger Logger { get; }
            public ModuleStates State { get; } = ModuleStates.Hollow;
            public string[] Args { get; }
        }
    }
}