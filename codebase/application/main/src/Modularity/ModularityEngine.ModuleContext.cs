using System;
using System.Collections.Generic;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
using System.Threading.Tasks;

using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Threading.Extensions.Tasks;


namespace Axle.Modularity
{
    partial class ModularityEngine
    {
        private sealed class ModuleContext
        {
            private ModuleContext(
                ModuleInfo moduleInfo, 
                object instance, 
                IDependencyContainer dependencyContainer, 
                ModuleExporter exporter, 
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

            public ModuleContext(ModuleInfo moduleInfo) : this(moduleInfo, null, null, null, null, new string[0]) { }
            private ModuleContext(
                    ModuleInfo moduleInfo, 
                    object instance, 
                    IDependencyContainer dependencyContainer, 
                    ModuleExporter exporter, 
                    ILogger logger, 
                    string[] args, 
                    ModuleState state) 
                : this(moduleInfo, instance, dependencyContainer, exporter, logger, args)
            {
                State = state;
            }

            public ModuleContext UpdateInstance(
                object moduleInstance, 
                IDependencyContainer dependencyContainer, 
                ModuleExporter exporter, 
                ILogger logger,
                string[] args) => new ModuleContext(ModuleInfo, moduleInstance, dependencyContainer, exporter, logger, args, State|ModuleState.Instantiated);

            private ModuleContext ChangeState(ModuleState state) => new ModuleContext(ModuleInfo, ModuleInstance, DependencyContainer, Exporter, Logger, Args, state);

            private void Notify(IList<ModuleInfo> requiredModules, IDictionary<Type, ModuleContext> moduleMetadata, Func<ModuleInfo, ModuleCallback[]> callbackProvider)
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

                        //parallelCallbacks.StartAll();

                        //try
                        //{
                        //    callback.Invoke(rmm.ModuleInstance, ModuleInstance);
                        //}
                        //catch (Exception e)
                        //{
                        //    // TODO: wrap exception
                        //    throw;
                        //}
                    }

                    parallelCallbacks.Start();
                    sequentialCallbacks.RunSynchronously();
                    parallelCallbacks.WaitForAll();
                    sequentialCallbacks.WaitForAll();
                }
            }

            public ModuleContext Init(IDictionary<Type, ModuleContext> moduleMetadata)
            {
                var exporter = Exporter;
                var requiredModules = ModuleInfo.RequiredModules;
                var args = Args;
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it has been terminated.");
                }
                if ((State & ModuleState.Instantiated) != ModuleState.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it has not been instantiated.");
                }
                if ((State & ModuleState.Initialized) == ModuleState.Initialized)
                {
                    Logger.Warn("Initialization attempted, but module `{0}` was already initialized. ", ModuleInfo.Type.FullName);
                    return this;
                }

                Logger.Debug("Initializing module `{0}`...", ModuleInfo.Type.FullName);

                ModuleInfo.InitMethod?.Invoke(ModuleInstance, exporter, args);
                var result = ChangeState(State | ModuleState.Initialized);
                Notify(requiredModules, moduleMetadata, m => m.DependencyInitializedMethods);

                Logger.Write(LogSeverity.Info, "Module `{0}` initialized.", ModuleInfo.Type.FullName);

                return result;
            }

            public ModuleContext Run()
            {
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed since it has been terminated.");
                }
                if ((State & ModuleState.Instantiated) != ModuleState.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be instantiated first. ");
                }
                if ((State & ModuleState.Initialized) != ModuleState.Initialized)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be initialized first.");
                }
                if ((State & ModuleState.Ran) == ModuleState.Ran)
                {   //
                    // Module is already ran
                    //
                    Logger.Warn("Execution attempted, but module `{0}` was already executed. ", ModuleInfo.Type.FullName);
                    return this;
                }
                ModuleInfo.EntryPointMethod?.Invoke(ModuleInstance, Args);
                return ChangeState(State | ModuleState.Ran);
            }

            public ModuleContext Terminate(IDictionary<Type, ModuleContext> moduleMetadata)
            {
                var exporter = Exporter;
                var requiredModules = ModuleInfo.RequiredModules;
                var args = Args;
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {   //
                    // Module is already terminated
                    //
                    Logger.Warn("Termination attempted, but module `{0}` was already terminated. ", ModuleInfo.Type.FullName);
                    return this;
                }

                Logger.Debug("Terminating module `{0}` ...", ModuleInfo.Type.FullName);

                Notify(requiredModules, moduleMetadata, m => m.DependencyTerminatedMethods);

                ModuleInfo.TerminateMethod?.Invoke(ModuleInstance, exporter, args);
                
                if (ModuleInstance is IDisposable d)
                {
                    d.Dispose();
                }
                
                var result = ChangeState(State | ModuleState.Terminated);

                Logger.Write(LogSeverity.Info, "Module `{0}` terminated. ", ModuleInfo.Type.FullName);

                return result;
            }

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public IDependencyContainer DependencyContainer { get; }
            public ModuleExporter Exporter { get; }
            public ILogger Logger { get; }
            public ModuleState State { get; } = ModuleState.Hollow;
            public string[] Args { get; }
        }
    }
}