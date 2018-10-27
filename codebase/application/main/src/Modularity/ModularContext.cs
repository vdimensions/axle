using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;

using Axle.DependencyInjection;
using Axle.Extensions.String;
using Axle.Logging;


namespace Axle.Modularity
{
    internal sealed partial class ModularContext : IDisposable
    {
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
                            @"Circular dependencies exist between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.GetType().FullName))));
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
        private readonly IContainer _rootContainer;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDependencyContainerProvider _containerProvier;
        private readonly ILoggingServiceProvider _loggingServiceProvider;
        private readonly IList<ModuleMetadata> _initializedModules = new List<ModuleMetadata>();

        public ModularContext(IModuleCatalog moduleCatalog, IDependencyContainerProvider containerProvier, ILoggingServiceProvider loggingServiceProvider)
        {
            _moduleCatalog = moduleCatalog;
            _containerProvier = containerProvier;
            _rootContainer = containerProvier.Create();
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
            var rootExporter = new ContainerExporter(_rootContainer);

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

                using (var moduleInitializationContainer = _containerProvier.Create(_rootContainer))
                {
                    var moduleLogger = _loggingServiceProvider.Create(moduleType);
                    var moduleContainer = _containerProvier.Create(_rootContainer);
                    moduleInitializationContainer
                            .RegisterInstance(moduleInfo)
                            .RegisterType(moduleType)
                            .RegisterInstance(moduleLogger)
                            .RegisterInstance(moduleContainer)
                            .RegisterInstance(rootExporter);

                    //
                    // Make all required modules injectable
                    //
                    for (var k = 0; k < requiredModules.Length; k++)
                    {
                        if (_modules.TryGetValue(requiredModules[k].Type, out var rmm))
                        {
                            moduleInitializationContainer.RegisterInstance(rmm.ModuleInstance);
                        }
                    }

                    // TODO: register configuration objects

                    var moduleInstance = moduleInitializationContainer.Resolve(moduleType);
                    var mm = _modules.AddOrUpdate(
                            moduleType,
                            _ =>
                            {
                                
                                var result = new ModuleMetadata(moduleInfo).UpdateInstance(moduleInstance, moduleInitializationContainer, moduleLogger);
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
                                    var result = m.UpdateInstance(moduleInstance, moduleInitializationContainer, moduleLogger);
                                    _moduleInstances.Push(result);
                                    return result;
                                }
                            });

                    try
                    {
                        mm = _modules.AddOrUpdate(moduleType, _ => mm.Init(rootExporter, requiredModules, _modules), (_, m) => m.Init(rootExporter, requiredModules, _modules));
                        _initializedModules.Add(mm);
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
            foreach (var initializedModule in _initializedModules)
            {
                _modules.TryUpdate(initializedModule.ModuleInfo.Type, initializedModule.Run(args), initializedModule);
            }
            _initializedModules.Clear();
            return this;
        }

        public void Dispose()
        {
            while (_moduleInstances.TryPop(out var module))
            {
                module.Terminate(new ContainerExporter(_rootContainer), module.ModuleInfo.RequiredModules.ToArray(), _modules);
                if (module.ModuleInstance is IDisposable d)
                {
                    d.Dispose();
                }
            }
            _rootContainer?.Dispose();
        }

        public IContainer Container => _rootContainer;
    }
}