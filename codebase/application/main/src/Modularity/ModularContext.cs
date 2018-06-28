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
            public ModuleMetadata(ModuleInfo moduleInfo, object instance)
            {
                ModuleInfo = moduleInfo;
                ModuleInstance = instance;
            }
            private ModuleMetadata(ModuleInfo moduleInfo, object instance, int notifiers) : this(moduleInfo, instance)
            {
                RemainingNotifiers = notifiers;
            }

            public ModuleMetadata AddNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, RemainingNotifiers + 1);
            public ModuleMetadata RemoveNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, RemainingNotifiers - 1);
            public ModuleMetadata UpdateInstance(object moduleInstance) => new ModuleMetadata(ModuleInfo, moduleInstance, RemainingNotifiers);

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; }
            public int RemainingNotifiers { get; } = 0;
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
        /// <returns>
        /// A collection of module groups, sorted by rank. 
        /// </returns>
        private static IEnumerable<IGrouping<int, ModuleInfo>> RankModules(IEnumerable<ModuleInfo> moduleCatalog)
        {
            var stringComparer = StringComparer.Ordinal;
            IDictionary<string, Tuple<ModuleInfo, int>> modulesWithRank = new Dictionary<string, Tuple<ModuleInfo, int>>(stringComparer);
            var modulesToLaunch = moduleCatalog.ToList();
            var remainingCount = int.MaxValue;
            while (modulesToLaunch.Count > 0)
            {
                if (remainingCount == modulesToLaunch.Count)
                {   //
                    // The remaining (unranked) modules count did not change for an iteration. This is a signal for a circular dependency.
                    //
                    throw new InvalidOperationException(
                            string.Format("A circular dependencies exists between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.Name))));
                }
                remainingCount = modulesToLaunch.Count;
                var modulesOfCurrentRank = new List<ModuleInfo>(modulesToLaunch.Count);
                foreach (var moduleInfo in modulesToLaunch)
                {
                    var rank = 0;
                    foreach (var moduleDependency in moduleInfo.RequiredModules)
                    {
                        if (modulesWithRank.TryGetValue(moduleDependency.Name, out var parentRank))
                        {
                            rank = Math.Max(rank, parentRank.Item2);
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
                    modulesWithRank[moduleInfo.Name] = Tuple.Create(moduleInfo, rank + 1);
                }
                modulesOfCurrentRank.ForEach(x => modulesToLaunch.Remove(x));
            }
            return modulesWithRank.Values.OrderBy(x => x.Item2).GroupBy(x => x.Item2, x => x.Item1);
        }

        private readonly ConcurrentDictionary<Type, ModuleMetadata> _modules = new ConcurrentDictionary<Type, ModuleMetadata>();
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDependencyContainerProvider _containerProvier;

        public ModularContext(IModuleCatalog moduleCatalog, IDependencyContainerProvider containerProvier)
        {
            _moduleCatalog = moduleCatalog;
            _containerProvier = containerProvier;
        }
        public ModularContext() : this(new DefaultModuleCatalog(), new DefaultDependencyContainerProvider()) { }

        public IContainer Launch(params Type[] moduleTypes)
        {
            var modules = new ConcurrentDictionary<Type, ModuleMetadata>();
            var moduleInfos = _moduleCatalog.GetModules(moduleTypes);

            //
            // Establish metadata for modules expecting callbacks
            //
            foreach (var m in moduleInfos)
            foreach (var rm in m.RequiredModules)
            {
                modules.AddOrUpdate(
                    rm.Type, 
                    _ => new ModuleMetadata(rm, null).AddNotifier(), 
                    (a, b) => b.AddNotifier());
            }

            var rankedModules = RankModules(moduleInfos).ToArray();
            var rootContainer = _containerProvier.Create();
            var rootExporter = new ContainerExporter(rootContainer);

            foreach (var rankGroup in rankedModules)
            foreach (var moduleInfo in rankGroup)
            {
                var moduleType = moduleInfo.Type;
                var requiredModules = moduleInfo.RequiredModules.ToArray();

                using (var moduleContainer = _containerProvier.Create(rootContainer))
                {
                    moduleContainer.RegisterInstance(moduleInfo).RegisterType(moduleType);

                    //
                    // Make all required modules injectable
                    //
                    foreach (var rm in requiredModules)
                    {
                        if (modules.TryGetValue(rm.Type, out var rmm))
                        {
                            moduleContainer.RegisterInstance(rmm.ModuleInstance);
                        }
                    }

                    // TODO: register configuration objects

                    var moduleInstance = moduleContainer.Resolve(moduleType);
                    var mm = modules.AddOrUpdate(
                        moduleType, 
                        new ModuleMetadata(moduleInfo, moduleInstance), 
                        (_, m) => m.ModuleInstance != null ? m : m.UpdateInstance(moduleInstance));
                    
                    try
                    {
                        mm.ModuleInfo.InitMethod?.Invoke(mm.ModuleInstance, rootExporter);
                        if (requiredModules.Length > 0)
                        {
                            foreach (var rm in requiredModules)
                            {
                                if (!modules.TryGetValue(rm.Type, out var rmm))
                                {
                                    continue;
                                }
                                foreach (var callback in rmm.ModuleInfo.DependencyInitializedMethods)
                                {
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
                                    rmm = modules.AddOrUpdate(rm.Type, _ => rmm.RemoveNotifier(), (_, m) => m.RemoveNotifier());
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
                        // TODO: termination
                        if (mm.ModuleInstance is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                        // TODO: throw proper exception
                        throw;
                    }
                }
            }

            return rootContainer;
        }
    }
}