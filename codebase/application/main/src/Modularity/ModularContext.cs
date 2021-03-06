﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Extensions.String;
using Axle.Logging;
using Axle.Text.Expressions.Substitution;


namespace Axle.Modularity
{
    internal sealed partial class ModularContext : IDisposable
    {
        private static IList<ModuleInfo> ExpandRequiredModules(IEnumerable<ModuleInfo> m)
        {
            var modulesToLaunch = m.ToList();
            var moduleInfoEqualityComparer = new AdaptiveEqualityComparer<ModuleInfo, Type>(x => x.Type);
            var requiredModules = modulesToLaunch.ToDictionary(mi => mi, _ => new HashSet<ModuleInfo>(moduleInfoEqualityComparer), moduleInfoEqualityComparer);
            for (var i = 0; i < modulesToLaunch.Count; i++)
            {
                var moduleInfo = modulesToLaunch[i];
                var expandedRequiredModules = requiredModules[moduleInfo];
                //
                // Expand the 'required modules' with the 'utilized modules'
                //
                foreach (var ua in moduleInfo.UtilizedModules)
                {
                    var newModules = modulesToLaunch.Where(mi => ua.ModuleType == mi.Type);
                    foreach (var newRequiredModule in newModules)
                    {
                        expandedRequiredModules.Add(newRequiredModule);
                    }
                }

                //
                // Expand the 'required modules' with the 'utilized-by modules'
                //
                var baseType = 
                    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                    moduleInfo.Type.BaseType
                    #else
                    System.Reflection.IntrospectionExtensions.GetTypeInfo(moduleInfo.Type).BaseType
                    #endif
                    ;
                var genType = baseType == null ? null :
                    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                    baseType
                    #else
                    System.Reflection.IntrospectionExtensions.GetTypeInfo(baseType)
                    #endif
                        .IsGenericType ? baseType.GetGenericTypeDefinition() : null;
                ModuleInfo genericModule = null;
                if (genType != null && genType == typeof(CollectorModule<>))
                {
                    var genericArg = 
                        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                        baseType
                        #else
                        System.Reflection.IntrospectionExtensions.GetTypeInfo(genType)
                        #endif
                        .GetGenericArguments()[0];
                    genericModule = modulesToLaunch.SingleOrDefault(x => x.Type == genericArg);
                    if (genericModule != null)
                    {
                        expandedRequiredModules.Add(genericModule);
                    }
                }
                foreach (var utilizedBy in modulesToLaunch
                    .SelectMany(x => x.UtilizedByModules.Select(u => new { UtilizedBy = u, Module = x }))
                    .Where(x => x.UtilizedBy.Accepts(moduleInfo.Type))
                    .Select(x => x.Module))
                {
                    if (genericModule != null)
                    {
                        requiredModules[utilizedBy].Add(genericModule);
                    }
                    expandedRequiredModules.Add(utilizedBy);
                }
            }

            foreach (var x in modulesToLaunch)
            {
                var newRequiredModules = requiredModules[x];
                if (newRequiredModules.Count <= 0)
                {
                    continue;
                }
                var newModules = new HashSet<ModuleInfo>(x.RequiredModules, moduleInfoEqualityComparer);
                foreach (var newRequiredModule in newRequiredModules)
                {
                    newModules.Remove(newRequiredModule);
                    newModules.Add(newRequiredModule);
                }
                x.RequiredModules = newModules.ToArray();
            }
            return modulesToLaunch;
        }
        private static IList<ModuleInfo> FilterTriggeredModules(IEnumerable<ModuleInfo> modules, IList<string> commandLineArgs)
        {
            var modulesToLaunch = modules.ToList();
            var cmp = StringComparer.Ordinal;
            var disabledModules = new HashSet<string>(cmp);
            for (var i = 0; i < modulesToLaunch.Count; i++)
            {
                var moduleInfo = modulesToLaunch[i];
                var moduleKey = UtilizesAttribute.TypeToString(moduleInfo.Type);
                var commandLineTrigger = moduleInfo.CommandLineTrigger;
                if (commandLineTrigger != null)
                {
                    if (commandLineArgs.Count <= commandLineTrigger.ArgumentIndex)
                    {
                        disabledModules.Add(moduleKey);
                    }
                    else if (!string.IsNullOrEmpty(commandLineTrigger.ArgumentValue) 
                        && !cmp.Equals(commandLineArgs[commandLineTrigger.ArgumentIndex], commandLineTrigger.ArgumentValue))
                    {
                        disabledModules.Add(moduleKey);
                    }
                }
            }

            if (disabledModules.Count <= 0)
            {
                return modulesToLaunch;
            }

            for (var i = 0; i < modulesToLaunch.Count; i++)
            {
                var moduleToFilter = modulesToLaunch[i];
                if (disabledModules.Contains(UtilizesAttribute.TypeToString(moduleToFilter.Type)))
                {
                    modulesToLaunch[i] = null;
                }
                else
                {
                    var filterRequiredModules = moduleToFilter.RequiredModules
                        .Where(x => !disabledModules.Contains(UtilizesAttribute.TypeToString(x.Type)))
                        .ToArray();
                    if (filterRequiredModules.Length != moduleToFilter.RequiredModules.Length)
                    {
                        modulesToLaunch[i] = null;
                    }
                }
            }

            return modulesToLaunch.Where(x => x != null).ToList();
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
        private static IEnumerable<IGrouping<int, ModuleInfo>> RankModules(
            IEnumerable<ModuleInfo> moduleCatalog, 
            ICollection<Type> loadedModules,
            IList<string> args)
        {
            IDictionary<Type, Tuple<ModuleInfo, int>> modulesWithRank = new Dictionary<Type, Tuple<ModuleInfo, int>>();
            var modulesToLaunch = FilterTriggeredModules(ExpandRequiredModules(moduleCatalog), args);
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
                var modulesToRemove = new List<ModuleInfo>(modulesToLaunch.Count);

                for (var i = 0; i < modulesToLaunch.Count; i++)
                {
                    var moduleInfo = modulesToLaunch[i];
                    int rank = 0;
                    foreach (var moduleDependency in moduleInfo.RequiredModules)
                    {
                        if (modulesWithRank.TryGetValue(moduleDependency.Type, out var parentRank))
                        {
                            rank = Math.Max(rank, parentRank.Item2);
                        }
                        else if (loadedModules.Contains(moduleDependency.Type))
                        {   //
                            // The module depends on an already loaded module. This will not contribute to rank increments.
                            //
                            rank = Math.Max(rank, 0);
                        }
                        else
                        {   //
                            // If a module has an unranked dependency, it cannot be ranked as well.
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
                    modulesToRemove.Add(moduleInfo);
                    modulesOfCurrentRank.Add(moduleInfo);
                    modulesWithRank[moduleInfo.Type] = Tuple.Create(moduleInfo, rank + 1);
                }
                modulesToRemove.ForEach(x => modulesToLaunch.Remove(x));
            }
            return modulesWithRank.Values.OrderBy(x => x.Item2).GroupBy(x => x.Item2, x => x.Item1);
        }

        private readonly ConcurrentDictionary<Type, ModuleMetadata> _modules = new ConcurrentDictionary<Type, ModuleMetadata>();
        private readonly ConcurrentStack<ModuleMetadata> _moduleInstances = new ConcurrentStack<ModuleMetadata>();
        private readonly IContainer _rootContainer;
        private readonly IDependencyContainerProvider _containerProvider;
        private readonly ILoggingServiceProvider _loggingServiceProvider;
        private readonly IList<ModuleMetadata> _initializedModules = new List<ModuleMetadata>();
        private readonly string[] _args;

        public ModularContext(IDependencyContainerProvider containerProvider, ILoggingServiceProvider loggingServiceProvider, IConfigSource appConfigurationSource, string[] args)
        {
            _containerProvider = containerProvider;
            _rootContainer = containerProvider.Create();
            _loggingServiceProvider = loggingServiceProvider;
            _args = args;
        }

        public ModularContext Launch(IModuleCatalog moduleCatalog, IConfigManager config, IEnumerable<Type> moduleTypes)
        {
            var moduleInfos = moduleCatalog.GetModules(moduleTypes);
            var existingModuleTypes = new HashSet<Type>(_modules.Keys);

            var rankedModules = RankModules(moduleInfos, existingModuleTypes, _args).ToArray();
            var rootExporter = new ContainerExporter(_rootContainer);

            var substExpr = new StandardSubstitutionExpression();
            IConfiguration globalAppConfig = 
                new SubstitutionResolvingConfig(
                    config.LoadConfiguration(),
                    substExpr);

            // TODO: strip ranked modules from non-activated

            _rootContainer.RegisterInstance(globalAppConfig);
            for (var i = 0; i < rankedModules.Length; i++)
            foreach (var moduleInfo in rankedModules[i])
            {
                var moduleType = moduleInfo.Type;
                if (existingModuleTypes.Contains(moduleType))
                {   //
                    // the module was initialized before
                    //
                    continue;
                }

                var requiredModules = moduleInfo.RequiredModules.ToArray();
                using (var moduleInitializationContainer = _containerProvider.Create(_rootContainer))
                {
                    var moduleLogger = _loggingServiceProvider.CreateLogger(moduleType);
                    var moduleContainer = _containerProvider.Create(_rootContainer);
                    moduleInitializationContainer
                        .RegisterType(moduleType)          // register the module type to be instantiated via DI
                        .RegisterInstance(moduleInfo)      // register moduleInfo so that a module can reflect on itself
                        .RegisterInstance(moduleLogger)    // register the module's dedicated logger
                        .RegisterInstance(moduleContainer) // register the module's dedicated DI container
                        .RegisterInstance(rootExporter);   // register the global dependencies exporter

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

                    var baseModuleConfig =
                        // TODO: prepend a module's embedded configuration file
                        // config.Prepend(...);
                        config;
                    IConfiguration moduleConfig = 
                        new SubstitutionResolvingConfig(
                            baseModuleConfig.LoadConfiguration(),
                            substExpr);
                    moduleInitializationContainer.RegisterInstance(moduleConfig);

                    if (moduleInfo.ConfigSectionInfo != null)
                    {   //
                        // if the module expects a configuration section from the global config
                        // auto-resolve it here and make it available for injection into the module
                        //
                        var csi = moduleInfo.ConfigSectionInfo;
                        var moduleConfigSection = ConfigSectionExtensions.GetSection(moduleConfig, csi.Name, csi.Type);
                        if (moduleConfigSection != null)
                        {
                            moduleInitializationContainer.RegisterInstance(moduleConfigSection);
                        }
                    }

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

                                var result = m.UpdateInstance(moduleInstance, moduleInitializationContainer, moduleLogger);
                                _moduleInstances.Push(result);
                                return result;
                            });

                    try
                    {
                        mm = _modules.AddOrUpdate(moduleType, _ => mm.Init(rootExporter, requiredModules, _modules, _args), (_, m) => m.Init(rootExporter, requiredModules, _modules, _args));
                        _initializedModules.Add(mm);
                    }
                    catch
                    {
                        mm = mm.Terminate(rootExporter, requiredModules, _modules, _args);
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

        public ModularContext Run()
        {
            foreach (var initializedModule in _initializedModules)
            {
                _modules.TryUpdate(initializedModule.ModuleInfo.Type, initializedModule.Run(_args), initializedModule);
            }
            _initializedModules.Clear();
            return this;
        }

        public void Dispose()
        {
            while (_moduleInstances.TryPop(out var module))
            {
                module.Terminate(new ContainerExporter(_rootContainer), module.ModuleInfo.RequiredModules.ToArray(), _modules, _args);
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