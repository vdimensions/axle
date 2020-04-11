using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Extensions.String;
using Axle.Verification;


namespace Axle.Modularity
{
    public static class ModuleCatalogExtensions
    {
        private static IList<ModuleInfo> ExpandRequiredModules(IList<ModuleInfo> modulesToLaunch)
        {
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

                //
                // The explicitly required modules must be added as a dependency to each `UtilizedBy` module,
                // in order to make sure the `UtilizedBy` module is not loaded before any of them.
                //
                var requiredSoFar = moduleInfo.RequiredModules;
                foreach (var utilizedBy in modulesToLaunch
                    .SelectMany(x => x.ReportsToModules.Select(u => new { UtilizedBy = u, Module = x }))
                    .Where(x => x.UtilizedBy.Accepts(moduleInfo.Type))
                    .Select(x => x.Module))
                {
                    for (var j = 0; j < requiredSoFar.Length; j++)
                    {
                        requiredModules[utilizedBy].Add(requiredSoFar[j]);
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
        
        private static IList<ModuleInfo> FilterTriggeredModules(IList<ModuleInfo> modules, IList<string> commandLineArgs)
        {
            var indicesToSkip = new HashSet<int>();
            var cmp = StringComparer.Ordinal;
            var disabledModules = new HashSet<string>(cmp);
            for (var i = 0; i < modules.Count; i++)
            {
                var moduleInfo = modules[i];
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
                return modules;
            }

            for (var i = 0; i < modules.Count; i++)
            {
                var moduleToFilter = modules[i];
                if (disabledModules.Contains(UtilizesAttribute.TypeToString(moduleToFilter.Type)))
                {
                    indicesToSkip.Add(i);
                }
                else
                {
                    var filterRequiredModules = moduleToFilter.RequiredModules
                        .Where(x => !disabledModules.Contains(UtilizesAttribute.TypeToString(x.Type)))
                        .ToArray();
                    if (filterRequiredModules.Length != moduleToFilter.RequiredModules.Length)
                    {
                        indicesToSkip.Add(i);
                    }
                }
            }

            return modules.Where((x, i) => !indicesToSkip.Contains(i)).ToList();
        }
        
        private static IList<ModuleInfo> DiscardModulesIncompatibleWithHost(
            IEnumerable<ModuleInfo> modules, 
            Type requiredApplicationHostType)
        {
            if (requiredApplicationHostType == null)
            {
                return modules.ToList();
            }
            return modules
                .Where(x => x.RequiredApplicationHostType == null || requiredApplicationHostType == x.RequiredApplicationHostType)
                .ToList();
        }
        
        private static void ExpandModules(IModuleCatalog catalog, Type moduleType, HashSet<Type> types)
        {
            if (!types.Add(moduleType))
            {
                return;
            }
            var modules = catalog.GetRequiredModules(moduleType);
            for (var i = 0; i < modules.Length; i++)
            {
                ExpandModules(catalog, modules[i], types);
            }
        }

        private static IEnumerable<ModuleInfo> ExtractModules(
                IModuleCatalog moduleCatalog, 
                IEnumerable<Type> types, 
                IDictionary<Type, ModuleInfo> knownModules)
        {
            var allModuleTypes = new HashSet<Type>();
            foreach (var v in types)
            {
                ExpandModules(moduleCatalog, v, allModuleTypes);
            }

            foreach (var moduleType in allModuleTypes)
            {
                var requiredModuleTypes = moduleCatalog.GetRequiredModules(moduleType).Except(new []{moduleType});
                var requiredModules = requiredModuleTypes
                        .Select(
                            t =>
                            {
                                if (!knownModules.TryGetValue(t, out var m))
                                {
                                    m = ExtractModules(moduleCatalog, new[] {t}, knownModules).Single(x => x.Type == t);
                                    knownModules.Add(t, m);
                                }
                                return m;
                            })
                        .ToArray();
                var utilizedModules = moduleCatalog.GetUtilizedModules(moduleType);
                var reportsToModules = moduleCatalog.GetReportsToModules(moduleType);
                var configurationInfo = moduleCatalog.GetConfigurationInfo(moduleType);
                ModuleInfo moduleInfo = null;
                try
                {
                    moduleInfo = new ModuleInfo(
                        moduleType,
                        moduleCatalog.GetRequiredApplicationHostType(moduleType),
                        moduleCatalog.GetInitMethod(moduleType),
                        moduleCatalog.GetDependencyInitializedMethods(moduleType),
                        moduleCatalog.GetDependencyTerminatedMethods(moduleType),
                        moduleCatalog.GetTerminateMethod(moduleType),
                        moduleCatalog.GetEntryPointMethod(moduleType),
                        utilizedModules,
                        reportsToModules,
                        moduleCatalog.GetCommandLineTrigger(moduleType),
                        configurationInfo,
                        requiredModules);
                }
                catch (Exception e)
                {
                    throw new ModuleDefinitionException(moduleType, e);
                }

                yield return moduleInfo;
            }
        }

        /// <summary>
        /// Creates a list of modules, grouped by and sorted by a rank integer. The rank determines
        /// the order for bootstrapping the modules -- those with lower rank will be initialized earlier. 
        /// A module with particular rank does not have module dependencies with a higher rank. 
        /// Modules of the same rank can be simultaneously initialized in multiple threads, if parallel option is
        /// specified.
        /// </summary>
        /// <param name="modulesToLaunch">
        /// An collection of modules to be ranked.
        /// </param>
        /// <param name="loadedModules">
        /// A list of already loaded modules, which will aid in ranking a subsequent set of modules to be loaded at runtime.
        /// </param>
        /// <returns>
        /// A collection of module groups, sorted by their rank in ascending order. 
        /// </returns>
        internal static IEnumerable<IGrouping<int, ModuleInfo>> RankModules(
            this IModuleCatalog moduleCatalog, 
            IList<ModuleInfo> modulesToLaunch, 
            ICollection<Type> loadedModules = null)
        {
            var loadedModulesSafe = loadedModules ?? new List<Type>();
            IDictionary<Type, Tuple<ModuleInfo, int>> modulesWithRank = new Dictionary<Type, Tuple<ModuleInfo, int>>();
            var remainingCount = int.MaxValue;
            while (modulesToLaunch.Count > 0)
            {
                if (remainingCount == modulesToLaunch.Count)
                {   //
                    // The count of the remaining (unranked) modules did not change for an iteration.
                    // This is a signal for a circular dependency.
                    //
                    throw new InvalidOperationException(
                        string.Format(
                            "Circular dependencies exist between some of the following modules: [{0}]", ", ".Join(modulesToLaunch.Select(x => x.GetType().FullName))));
                }

                remainingCount = modulesToLaunch.Count;
                var modulesOfCurrentRank = new List<ModuleInfo>(modulesToLaunch.Count);
                var modulesToRemove = new List<ModuleInfo>(modulesToLaunch.Count);

                for (var i = 0; i < modulesToLaunch.Count; i++)
                {
                    var moduleInfo = modulesToLaunch[i];
                    var rank = 0;
                    foreach (var moduleDependency in moduleInfo.RequiredModules)
                    {
                        if (modulesWithRank.TryGetValue(moduleDependency.Type, out var parentRank))
                        {
                            rank = Math.Max(rank, parentRank.Item2);
                        }
                        else if (loadedModulesSafe.Contains(moduleDependency.Type))
                        {   //
                            // The module depends on an already loaded module. This will not contribute to rank
                            // increments.
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
                        // The module has dependencies which are not ranked yet, meaning it must survive another
                        // iteration to determine rank.
                        //
                        continue;
                    }

                    //
                    // The module has no unranked dependencies, so its rank is determined as the highest rank of its
                    // existing dependencies plus one. Modules with no dependencies will have a rank of 1.
                    //
                    modulesToRemove.Add(moduleInfo);
                    modulesOfCurrentRank.Add(moduleInfo);
                    modulesWithRank[moduleInfo.Type] = Tuple.Create(moduleInfo, rank + 1);
                }
                modulesToRemove.ForEach(x => modulesToLaunch.Remove(x));
            }
            return modulesWithRank.Values
                .OrderBy(x => x.Item2)
                .GroupBy(x => x.Item2, x => x.Item1);
        }

        internal static IList<ModuleInfo> GetModules(
            this IModuleCatalog moduleCatalog, 
            IEnumerable<Type> types,
            Type requiredApplicationHostType,
            params string[] commandLineArgs)
        {
            moduleCatalog.VerifyArgument(nameof(moduleCatalog)).IsNotNull();
            var extractedModules = ExtractModules(moduleCatalog, types, new Dictionary<Type, ModuleInfo>());
            var hostableModules = DiscardModulesIncompatibleWithHost(extractedModules, requiredApplicationHostType);
            var expandedModules = ExpandRequiredModules(hostableModules);
            return FilterTriggeredModules(expandedModules, commandLineArgs);
        }
    }
}