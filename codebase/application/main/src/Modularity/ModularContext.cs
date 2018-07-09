using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Threading.Tasks;

using Axle.Application.DependencyInjection;
using Axle.Application.Logging;
using Axle.Extensions.String;


namespace Axle.Application.Modularity
{
    internal sealed class ModularContext : IDisposable
    {
        private sealed class ContainerExporter : ModuleExporter
        {
            private readonly IContainer _container;

            public ContainerExporter(IContainer container)
            {
                _container = container;
            }

            public override ModuleExporter Export<T>(T instance, string name)
            {
                _container.RegisterInstance(instance, name);
                return this;
            }

            public override ModuleExporter Export<T>(T instance)
            {
                _container.RegisterInstance(instance);
                return this;
            }
        }

        [Flags]
        private enum ModuleState : sbyte
        {
            Hollow = 0,
            Instantiated = 1,
            Initialized = 2,
            Ran = 4,
            Terminated = -1,
        }

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
                    //List<Task> parallelCallbacks = new List<Task>(callbacks.Length);
                    //List<Task> sequentialCallbacks = new List<Task>(callbacks.Length);
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

                        //var listToAddTaskTo = callback.AllowParallelInvoke ? parallelCallbacks : sequentialCallbacks;
                        //listToAddTaskTo.Add(new Task(() => callback.Invoke(rmm.ModuleInstance, ModuleInstance)));

                        try
                        {
                            callback.Invoke(rmm.ModuleInstance, ModuleInstance);
                        }
                        catch (Exception e)
                        {
                            // TODO: wrap exception
                            throw;
                        }
                    }

                    //parallelCallbacks.StartAll();
                }
            }

            public ModuleMetadata Init(ModuleExporter exporter, ModuleInfo[] requiredModules, IDictionary<Type, ModuleMetadata> moduleMetadata)
            {
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it is was terminated.");
                }
                if ((State & ModuleState.Instantiated) != ModuleState.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be initialized since it is was not instantiated.");
                }
                if ((State & ModuleState.Initialized) == ModuleState.Initialized)
                {
                    Logger.Warn("Initialization attempted, but module was already initialized. ");
                    return this;
                }

                Logger.Debug("Initializing module ...");

                ModuleInfo.InitMethod?.Invoke(ModuleInstance, exporter);
                var result = ChangeState(State | ModuleState.Initialized);
                Notify(requiredModules, moduleMetadata, m => m.DependencyInitializedMethods);

                Logger.Write(LogSeverity.Info, "Module initialization complete. ");

                return result;
            }

            public ModuleMetadata Run(params string[] args)
            {
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed since it is was terminated.");
                }
                if ((State & ModuleState.Instantiated) != ModuleState.Instantiated)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be instantiated first. ");
                }
                if ((State & ModuleState.Initialized) == ModuleState.Initialized)
                {
                    throw new InvalidOperationException($"Module `{ModuleInfo.Type.FullName}` cannot be executed. It must be initialized first.");
                }
                if ((State & ModuleState.Ran) == ModuleState.Ran)
                {   //
                    // Module is already ran
                    //
                    Logger.Warn("Execution attempted, but module was already executed. ");
                    return this;
                }
                ModuleInfo.EntryPointMethod?.Invoke(ModuleInstance, args);
                return ChangeState(State | ModuleState.Ran);
            }

            public ModuleMetadata Terminate(ModuleExporter exporter, ModuleInfo[] requiredModules, IDictionary<Type, ModuleMetadata> moduleMetadata)
            {
                if ((State & ModuleState.Terminated) == ModuleState.Terminated)
                {   //
                    // Module is already terminated
                    //
                    Logger.Warn("Termination attempted, but module was already terminated. ");
                    return this;
                }

                Logger.Debug("Terminating module ...");

                Notify(requiredModules, moduleMetadata, m => m.DependencyTerminatedMethods);

                ModuleInfo.TerminateMethod?.Invoke(ModuleInstance, exporter);
                var result = ChangeState(State | ModuleState.Terminated);

                Logger.Write(LogSeverity.Info, "Module termination complete. ");

                return result;
            }

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public IContainer Container { get; }
            public ILogger Logger { get; }
            public ModuleState State { get; } = ModuleState.Hollow;
        }

        /// <summary>
        /// Creates a list of modules, grouped by and sorted by a rank integer. The rank determines
        /// the order for bootstrapping the modules -- those with lower rank will be bootstrapped earlier. 
        /// A module with particular rank does not have module dependencies with a higher rank. 
        /// Modules of the same rank can be bootstrapped simultaneously in multiple threads, if parallel option is specified.
        /// </summary>
        /// <param name="moduleCatalog">
        /// An collection of modules to be ranked.
        /// </param>
        /// <param name="loadedModules">
        /// A list of already loaded modules, which will aid in ranking a subsequent set of modules to be loaded at runtime.
        /// </param>
        /// <returns>
        /// A collection of module groups, sorted by rank. 
        /// </returns>
        private static IEnumerable<IGrouping<int, ModuleInfo>> RankModules(IEnumerable<ModuleInfo> moduleCatalog, ICollection<Type> loadedModules)
        {
            IDictionary<Type, Tuple<ModuleInfo, int>> modulesWithRank = new Dictionary<Type, Tuple<ModuleInfo, int>>();
            var modulesToLaunch = moduleCatalog.ToList();
            var remainingCount = int.MaxValue;
            while (modulesToLaunch.Count > 0)
            {
                if (remainingCount == modulesToLaunch.Count)
                {   //
                    // The remaining (unranked) modules count did not change for an iteration. This is a signal for a circular dependency.
                    //
                    throw new InvalidOperationException(
                        string.Format(
                            @"Circular dependencies exists between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.GetType().FullName))));
                }
                remainingCount = modulesToLaunch.Count;
                var modulesOfCurrentRank = new List<ModuleInfo>(modulesToLaunch.Count);
                for (var i = 0; i < modulesToLaunch.Count; i++)
                {
                    var moduleInfo = modulesToLaunch[i];
                    var rank = 0;
                    for (var j = 0; j < moduleInfo.RequiredModules.Length; j++)
                    {
                        var moduleDependency = moduleInfo.RequiredModules[j];
                        if (modulesWithRank.TryGetValue(moduleDependency.Type, out var parentRank))
                        {
                            rank = Math.Max(rank, parentRank.Item2);
                        }
                        else if (loadedModules.Contains(moduleDependency.Type))
                        {   //
                            // The module depends on an already loaded module.
                            //
                            rank = Math.Max(rank, 0);
                        }
                        else
                        {   //
                            // The module has an unranked dependency, it cannot be ranked as well.
                            //
                            rank = -1;
                            break;
                        }
                    }

                    if (rank < 0)
                    {   //
                        // The module has dependencies which are not ranked yet, meaning it must survive another iteration to determine rank.
                        //
                        continue;
                    }

                    //
                    // The module has no unranked dependencies, so its rank is determined as the highest rank of its existing dependencies plus one.
                    // Modules with no dependencies will have a rank of 1.
                    //
                    modulesOfCurrentRank.Add(moduleInfo);
                    modulesWithRank[moduleInfo.Type] = Tuple.Create(moduleInfo, rank + 1);
                }

                modulesOfCurrentRank.ForEach(x => modulesToLaunch.Remove(x));
            }
            return modulesWithRank.Values.OrderBy(x => x.Item2).GroupBy(x => x.Item2, x => x.Item1);
        }

        private readonly ConcurrentDictionary<Type, ModuleMetadata> _modules = new ConcurrentDictionary<Type, ModuleMetadata>();
        private readonly ConcurrentStack<ModuleMetadata> _moduleInstances = new ConcurrentStack<ModuleMetadata>();
        private readonly IContainer _moduleContainer;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDependencyContainerProvider _containerProvier;
        private readonly ILoggingServiceProvider _loggingServiceProvider;

        public ModularContext(IModuleCatalog moduleCatalog, IDependencyContainerProvider containerProvier, ILoggingServiceProvider loggingServiceProvider)
        {
            _moduleCatalog = moduleCatalog;
            _containerProvier = containerProvier;
            _moduleContainer = containerProvier.Create();
            _loggingServiceProvider = loggingServiceProvider;
        }
        public ModularContext() : this(
                  new DefaultModuleCatalog(), 
                  new DefaultDependencyContainerProvider(), 
                  new DefaultLoggingServiceProvider()) { }

        public ModularContext Launch(params Type[] moduleTypes)
        {
            var moduleInfos = _moduleCatalog.GetModules(moduleTypes);
            var existingModuleTypes = new HashSet<Type>(_modules.Keys);

            var rankedModules = RankModules(moduleInfos, existingModuleTypes).ToArray();
            var rootContainer = _moduleContainer;
            var rootExporter = new ContainerExporter(rootContainer);

            for (var i = 0; i < rankedModules.Length; i++)
            foreach (var moduleInfo in rankedModules[i])
            {
                var moduleType = moduleInfo.Type;
                var requiredModules = moduleInfo.RequiredModules.ToArray();
                if (existingModuleTypes.Contains(moduleType))
                {   //
                    // the module was initialized before
                    //
                    continue;
                }

                using (var moduleContainer = _containerProvier.Create(rootContainer))
                {
                    var moduleLogger = _loggingServiceProvider.Create(moduleType);
                    moduleContainer
                            .RegisterInstance(moduleInfo)
                            .RegisterType(moduleType)
                            .RegisterInstance(moduleLogger);

                    //
                    // Make all required modules injectable
                    //
                    for (var k = 0; k < requiredModules.Length; k++)
                    {
                        var rm = requiredModules[k];
                        if (_modules.TryGetValue(rm.Type, out var rmm))
                        {
                            moduleContainer.RegisterInstance(rmm.ModuleInstance);
                        }
                    }

                    // TODO: register configuration objects

                    var moduleInstance = moduleContainer.Resolve(moduleType);
                    var mm = _modules.AddOrUpdate(
                            moduleType,
                            _ =>
                            {
                                
                                var result = new ModuleMetadata(moduleInfo).UpdateInstance(moduleInstance, moduleContainer, moduleLogger);
                                _moduleInstances.Push(result);
                                return result;
                            },
                            (_, m) =>
                            {
                                if (m.ModuleInstance != null)
                                {
                                    return m;
                                }
                                else
                                {
                                    var result = m.UpdateInstance(moduleInstance, moduleContainer, moduleLogger);
                                    _moduleInstances.Push(result);
                                    return result;
                                }
                            });

                    try
                    {
                        mm = _modules.AddOrUpdate(moduleType, _ => mm.Init(rootExporter, requiredModules, _modules), (_, m) => m.Init(rootExporter, requiredModules, _modules));
                    }
                    catch
                    {
                        mm = mm.Terminate(rootExporter, requiredModules, _modules);
                        if (mm.ModuleInstance is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }

                        // TODO: throw proper exception
                        throw;
                    }
                }
            }

            return this;
        }

        public ModularContext Run(params string[] args)
        {
            // TODO: sort by rank before execution
            return this;
        }

        public void Dispose()
        {
            while (_moduleInstances.TryPop(out var module))
            {
                module.Terminate(new ContainerExporter(_moduleContainer), module.ModuleInfo.RequiredModules.ToArray(), _modules);
                if (module.ModuleInstance is IDisposable d)
                {
                    d.Dispose();
                }
            }
            _moduleContainer?.Dispose();
        }
    }
}