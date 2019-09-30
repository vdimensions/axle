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
    partial class ModularContext
    {
        private sealed class ModuleMetadata
        {
            private ModuleMetadata(ModuleInfo moduleInfo, object instance, IContainer container, ILogger logger)
            {
                ModuleInfo = moduleInfo;
                ModuleInstance = instance;
                Container = container;
                Logger = logger;
            }
            public ModuleMetadata(ModuleInfo moduleInfo) : this(moduleInfo, null, null, null) { }
            private ModuleMetadata(ModuleInfo moduleInfo, object instance, IContainer container, ILogger logger, ModuleState state) : this(moduleInfo, instance, container, logger)
            {
                State = state;
            }

            public ModuleMetadata UpdateInstance(object moduleInstance, IContainer container, ILogger logger) => new ModuleMetadata(ModuleInfo, moduleInstance, container, logger, State|ModuleState.Instantiated);

            private ModuleMetadata ChangeState(ModuleState state) => new ModuleMetadata(ModuleInfo, ModuleInstance, Container, Logger, state);

            private void Notify(ModuleInfo[] requiredModules, IDictionary<Type, ModuleMetadata> moduleMetadata, Func<ModuleInfo, ModuleCallback[]> callbackProvider)
            {
                if (requiredModules.Length <= 0)
                {
                    return;
                }
                for (var k = 0; k < requiredModules.Length; k++)
                {
                    var rm = requiredModules[k];
                    if (!moduleMetadata.TryGetValue(rm.Type, out var rmm))
                    {
                        continue;
                    }

                    var callbacks = callbackProvider(rmm.ModuleInfo);
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
                        listToAddTaskTo.Add(new Task(() => callback.Invoke(rmm.ModuleInstance, ModuleInstance)));

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

            public ModuleMetadata Init(ModuleExporter exporter, ModuleInfo[] requiredModules, IDictionary<Type, ModuleMetadata> moduleMetadata, string[] args)
            {
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

            public ModuleMetadata Run(params string[] args)
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
                ModuleInfo.EntryPointMethod?.Invoke(ModuleInstance, args);
                return ChangeState(State | ModuleState.Ran);
            }

            public ModuleMetadata Terminate(ModuleExporter exporter, ModuleInfo[] requiredModules, IDictionary<Type, ModuleMetadata> moduleMetadata, string[] args)
            {
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
                var result = ChangeState(State | ModuleState.Terminated);

                Logger.Write(LogSeverity.Info, "Module `{0}` terminated. ", ModuleInfo.Type.FullName);

                return result;
            }

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public IContainer Container { get; }
            public ILogger Logger { get; }
            public ModuleState State { get; } = ModuleState.Hollow;
        }
    }
}