using System;
using System.Collections.Generic;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
#if !UNITY_WEBGL
using System.Linq;
using System.Threading.Tasks;
#endif
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
#if !UNITY_WEBGL
using Axle.Threading.Extensions.Tasks;
#endif

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
                    var parallelCallbacks = new List<Action>(callbacks.Length);
                    var sequentialCallbacks = new List<Action>(callbacks.Length);
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
                        listToAddTaskTo.Add(() => callback.Invoke(targetContext.ModuleInstance, ModuleInstance));
                    }
                    
                    #if !UNITY_WEBGL
                    var tasks = parallelCallbacks.Select(cb => new Task(cb));
                    tasks.Start(TaskScheduler.Default);
                    foreach (var callback in sequentialCallbacks)
                    {
                        callback();
                    }
                    tasks.WaitForAll();
                    #else
                    foreach (var callback in parallelCallbacks)
                    {
                        callback();
                    }
                    foreach (var callback in sequentialCallbacks)
                    {
                        callback();
                    }
                    #endif
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
                    Logger.Warn("Attempted to initialize an already initialized module: `{0}`. ", ModuleInfo.Type.FullName);
                    return this;
                }

                //Logger.Debug("Module `{0}` initializing...", ModuleInfo.Type.FullName);

                ModuleInfo.InitMethod?.Invoke(ModuleInstance, exporter, args);
                var result = ChangeState(State | ModuleStates.Initialized);
                Notify(requiredModules, moduleMetadata, m => m.DependencyInitializedMethods);

                Logger.Debug("Module `{0}` initialized.", ModuleInfo.Type.FullName);

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
                    Logger.Warn("Attempted to prepare an already prepared module: `{0}`. ", ModuleInfo.Type.FullName);
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
                    Logger.Warn("Attempted to execute an already executed module: `{0}`. ", ModuleInfo.Type.FullName);
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
                    Logger.Warn("Attempted to terminate an already terminated module: `{0}`. ", ModuleInfo.Type.FullName);
                    return this;
                }

                //Logger.Debug("Module `{0}` terminating...", ModuleInfo.Type.FullName);

                Notify(requiredModules, moduleMetadata, m => m.DependencyTerminatedMethods);

                ModuleInfo.TerminateMethod?.Invoke(ModuleInstance, exporter, args);
                
                if (ModuleInstance is IDisposable d)
                {
                    d.Dispose();
                }
                
                var result = ChangeState(State | ModuleStates.Terminated);

                Logger.Debug("Module `{0}` terminated. ", ModuleInfo.Type.FullName);

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