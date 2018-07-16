using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Modularity
{
    public static class ModuleCatalogExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        public static ModuleInfo[] GetModules(this IModuleCatalog moduleCatalog)
        {
            return GetModules(moduleCatalog, moduleCatalog.DiscoverModuleTypes());
        }
        #endif

        public static ModuleInfo[] GetModules(this IModuleCatalog moduleCatalog, params Type[] types)
        {
            moduleCatalog.VerifyArgument(nameof(moduleCatalog)).IsNotNull();
            return ExtractModules(moduleCatalog, types, new Dictionary<Type, ModuleInfo>()).ToArray();
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
                IList<Type> types, 
                IDictionary<Type, ModuleInfo> knownModules)
        {
            var allModuleTypes = new HashSet<Type>();
            for (var i = 0; i < types.Count; i++)
            {
                ExpandModules(moduleCatalog, types[i], allModuleTypes);
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
                var module = new ModuleInfo(
                        moduleType,
                        moduleCatalog.GetInitMethod(moduleType),
                        moduleCatalog.GetDependencyInitializedMethods(moduleType),
                        moduleCatalog.GetDependencyTerminatedMethods(moduleType),
                        moduleCatalog.GetTerminateMethod(moduleType),
                        moduleCatalog.GetEntryPointMethod(moduleType),
                        requiredModules);
                yield return module;
            }
        }
    }
}