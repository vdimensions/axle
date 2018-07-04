using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Application.DependencyInjection;
using Axle.Extensions.String;


namespace Axle.Application.Modularity
{
    public class ModularContext
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

        private sealed class ModuleMetadata
        {
            public ModuleMetadata(ModuleInfo moduleInfo, object instance, IContainer container)
            {
                ModuleInfo = moduleInfo;
                ModuleInstance = instance;
                Container = container;
            }
            private ModuleMetadata(ModuleInfo moduleInfo, object instance, IContainer container, int notifiers) : this(moduleInfo, instance, container)
            {
                RemainingNotifiers = notifiers;
            }

            public ModuleMetadata AddNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, Container, RemainingNotifiers + 1);
            public ModuleMetadata RemoveNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, Container, RemainingNotifiers - 1);
            public ModuleMetadata UpdateInstance(object moduleInstance, IContainer container) => new ModuleMetadata(ModuleInfo, moduleInstance, container, RemainingNotifiers);

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public int RemainingNotifiers { get; } = 0;
            public IContainer Container { get; }
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
                            @"A circular dependencies exists between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.GetType().FullName))));
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

        private readonly ConcurrentDictionary<Type, ModuleMetadata[]> _moduleDependencies = new ConcurrentDictionary<Type, ModuleMetadata[]>();
        private readonly ConcurrentDictionary<Type, ModuleMetadata> _modules = new ConcurrentDictionary<Type, ModuleMetadata>();
        private readonly IContainer _moduleContainer;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDependencyContainerProvider _containerProvier;

        public ModularContext(IModuleCatalog moduleCatalog, IDependencyContainerProvider containerProvier)
        {
            _moduleCatalog = moduleCatalog;
            _containerProvier = containerProvier;
            _moduleContainer = containerProvier.Create();
        }
        public ModularContext() : this(new DefaultModuleCatalog(), new DefaultDependencyContainerProvider()) { }

        public IContainer Launch(params Type[] moduleTypes)
        {
            var modules = new ConcurrentDictionary<Type, ModuleMetadata>();
            var moduleInfos = _moduleCatalog.GetModules(moduleTypes);
            var existingModuleTypes = new HashSet<Type>(_modules.Keys);

            //
            // Establish metadata for modules expecting callbacks
            //
            for (var i = 0; i < moduleInfos.Length; i++)
            for (var j = 0; j < moduleInfos[i].RequiredModules.Length; j++)
            {
                var rm = moduleInfos[i].RequiredModules[j];
                modules.AddOrUpdate(
                        rm.Type,
                        t =>
                        {
                            var result = _modules.TryGetValue(t, out var existingMM) ? existingMM : new ModuleMetadata(rm, null, null);
                            if (!existingModuleTypes.Contains(moduleInfos[i].Type))
                            {   //
                                // if the current module is not previously initialized, it is eligible to notify its required modules.
                                //
                                result = result.AddNotifier();
                            }
                            return result;
                        },
                        (a, b) => 
                        {
                            if (!existingModuleTypes.Contains(moduleInfos[i].Type))
                            {   //
                                // if the current module is not previously initialized, it is eligible to notify its required modules.
                                //
                                return b.AddNotifier();
                            }
                            return b;
                        });
            }

            
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
                    moduleContainer.RegisterInstance(moduleInfo).RegisterType(moduleType);

                    //
                    // Make all required modules injectable
                    //
                    for (var k = 0; k < requiredModules.Length; k++)
                    {
                        var rm = requiredModules[k];
                        if (modules.TryGetValue(rm.Type, out var rmm))
                        {
                            moduleContainer.RegisterInstance(rmm.ModuleInstance);
                        }
                    }

                    // TODO: register configuration objects

                    var moduleInstance = moduleContainer.Resolve(moduleType);
                    var mm = modules.AddOrUpdate(
                            moduleType,
                            new ModuleMetadata(moduleInfo, moduleInstance, moduleContainer),
                            (_, m) => m.ModuleInstance != null ? m : m.UpdateInstance(moduleInstance, moduleContainer));

                    try
                    {
                        mm.ModuleInfo.InitMethod?.Invoke(mm.ModuleInstance, rootExporter);
                        if (requiredModules.Length > 0)
                        {
                            for (var k = 0; k < requiredModules.Length; k++)
                            {
                                var rm = requiredModules[k];
                                if (!modules.TryGetValue(rm.Type, out var rmm))
                                {
                                    continue;
                                }

                                for (var l = 0; l < rmm.ModuleInfo.DependencyInitializedMethods.Length; l++)
                                {
                                    var callback = rmm.ModuleInfo.DependencyInitializedMethods[l];
                                    #if NETSTANDARD || NET45_OR_NEWER
                                    if (!callback.ArgumentType.GetTypeInfo().IsInstanceOfType(mm.ModuleInstance))
                                    #else
                                    if (!callback.ArgumentType.IsInstanceOfType(mm.ModuleInstance))
                                    #endif
                                    {
                                        continue;
                                    }

                                    try
                                    {
                                        callback.Invoke(rmm.ModuleInstance, mm.ModuleInstance);
                                    }
                                    catch (Exception e)
                                    {
                                        // TODO: wrap exception
                                        throw;
                                    }
                                }

                                rmm = modules.AddOrUpdate(rm.Type, _ => rmm.RemoveNotifier(), (_, m) => m.RemoveNotifier());
                                if (rmm.RemainingNotifiers != 0)
                                {
                                    continue;
                                }

                                try
                                {
                                    rmm.ModuleInfo.ReadyMethod.Invoke(rmm.ModuleInstance, rootExporter);
                                    //
                                    // Ensure that rmm.RemainingNotifiers is negative to guarantee a single ready call.
                                    //
                                    modules.AddOrUpdate(rm.Type, _ => rmm.RemoveNotifier(), (_, m) => m.RemoveNotifier());
                                }
                                catch (Exception e)
                                {
                                    // TODO: wrap exception
                                    throw;
                                }
                            }
                        }

                        if (mm.RemainingNotifiers == 0)
                        {
                            mm.ModuleInfo.ReadyMethod?.Invoke(mm.ModuleInstance, rootExporter);
                            //
                            // mm.RemainingNotifiers will become negative, indicating the ready method is already called
                            // 
                            mm = modules.AddOrUpdate(moduleType, _ => mm.RemoveNotifier(), (_, m) => m.RemoveNotifier());
                        }
                    }
                    catch
                    {
                        // TODO: termination method execution

                        if (mm.ModuleInstance is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }

                        // TODO: throw proper exception
                        throw;
                    }
                }
            }

            foreach (var moduleMetadata in modules.Values)
            {
                if (!_modules.TryAdd(moduleMetadata.ModuleInfo.Type, moduleMetadata))
                {
                    // TODO: concurrent module initialization of the same module
                }
            }

            return rootContainer;
        }
    }
}