using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Axle.Application.DependencyInjection;
using Axle.Extensions.String;


namespace Axle.Application.Modularity
{
    public class ModuleCatalog
    {
        private class ModuleExporter : IModuleExporter
        {
            private readonly IContainer _container;

            public ModuleExporter(IContainer container)
            {
                _container = container;
            }

            public IModuleExporter Export<T>(T instance, string name)
            {
                _container.RegisterInstance(instance, name);
                return this;
            }

            public IModuleExporter Export<T>(T instance)
            {
                _container.RegisterInstance(instance);
                return this;
            }
        }

        private class ModuleMetadata
        {
            public ModuleMetadata(ModuleInfo moduleInfo)
            {
                ModuleInfo = moduleInfo;
            }
            private ModuleMetadata(ModuleInfo moduleInfo, object instance, int notifiers) : this(moduleInfo)
            {
                ModuleInstance = instance;
                PotentialNotifiers = notifiers;
            }

            public ModuleMetadata SetInstance(object instance) => new ModuleMetadata(ModuleInfo, instance, PotentialNotifiers);
            public ModuleMetadata AddNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, PotentialNotifiers + 1);
            public ModuleMetadata RemoveNotifier() => new ModuleMetadata(ModuleInfo, ModuleInstance, PotentialNotifiers - 1);

            public ModuleInfo ModuleInfo { get; }
            public object ModuleInstance { get; } = null;
            public int PotentialNotifiers { get; } = 0;
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

        public IContainer LaunchModules(IReflectionProvider reflectionProvider, IDependencyContainerProvider containerProvier)
        {
            var modules = new ConcurrentDictionary<Type, ModuleMetadata>();
            var moduleInfos = reflectionProvider.GetModules();
            foreach (var m in moduleInfos)
            foreach (var rm in m.RequiredModules)
            {
                modules.AddOrUpdate(rm.Type, _ => new ModuleMetadata(rm), (a, b) => b.AddNotifier());
            }

            var rankedModules = RankModules(moduleInfos).ToArray();
            var rootContainer = containerProvier.Create();
            var rootExporter = new ModuleExporter(rootContainer);

            //var deps = new ConcurrentDictionary<Type, IEnumerable<ModuleMetadata>>();
            foreach (var rankGroup in rankedModules)
            foreach (var moduleInfo in rankGroup)
            {
                using (var moduleContainer = containerProvier.Create(rootContainer))
                {
                    var moduleType = moduleInfo.Type;
                    moduleContainer.RegisterInstance(moduleInfo);
                    moduleContainer.RegisterInstance(moduleInfo.Name);
                    moduleContainer.RegisterType(moduleType);
                    // TODO: register configuration

                    var moduleInstance = moduleContainer.Resolve(moduleType);
                    modules.AddOrUpdate(moduleType, new ModuleMetadata(moduleInfo).SetInstance(moduleInstance), (_, m) => m.SetInstance(moduleInstance));
                }
            }

            foreach (var mm in modules.Values.ToList())
            {
                try
                {
                    mm.ModuleInfo.InitMethod.Invoke(mm.ModuleInstance, rootExporter);
                    if (mm.PotentialNotifiers == 0)
                    {
                        mm.ModuleInfo.OnDependenciesReadyMethod.Invoke(mm.ModuleInstance, rootExporter);
                    }
                    var requiredMethods = mm.ModuleInfo.RequiredModules.ToArray();
                    if (requiredMethods.Length > 0)
                    {
                        foreach (var rm in requiredMethods)
                        {
                            if (modules.TryGetValue(rm.Type, out var rmm))
                            {
                                foreach (var notifyMethiod in rmm.ModuleInfo.OnDependencyInitializedMethods)
                                {
                                    if (!notifyMethiod.ArgumentType.IsInstanceOfType(mm.ModuleInstance))
                                    {
                                        continue;
                                    }

                                    try
                                    {
                                        notifyMethiod.Invoke(rmm.ModuleInstance, mm.ModuleInstance);
                                    }
                                    catch (Exception e)
                                    {
                                        // TODO: wrap exception
                                        throw;
                                    }
                                }
                                rmm = modules.AddOrUpdate(rm.Type, _ => rmm, (_, m) => m.RemoveNotifier());
                                if (rmm.PotentialNotifiers == 0)
                                {
                                    try
                                    {
                                        rmm.ModuleInfo.OnDependenciesReadyMethod.Invoke(rmm.ModuleInstance, rootExporter);
                                    }
                                    catch (Exception e)
                                    {
                                        // TODO: wrap exception
                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (mm.ModuleInstance is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    // TODO: throw proper exception
                    throw;
                }
            }

            return rootContainer;
        }
    }
}