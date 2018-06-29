using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Application.Modularity
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
            types.Add(moduleType);
            foreach (var type in catalog.GetRequiredModules(moduleType))
            {
                ExpandModules(catalog, type, types);
            }
        }

        private static IEnumerable<ModuleInfo> ExtractModules(
                IModuleCatalog moduleCatalog, 
                IEnumerable<Type> types, 
                IDictionary<Type, ModuleInfo> knownModules)
        {
            var allModuleTypes = new HashSet<Type>();
            foreach (var type in types)
            {
                ExpandModules(moduleCatalog, type, allModuleTypes);
            }
            foreach (var moduleType in allModuleTypes)
            {
                var requiredModuleTypes = moduleCatalog.GetRequiredModules(moduleType);
                var requiredModules = requiredModuleTypes
                        .Select(
                            t =>
                            {
                                if (!knownModules.TryGetValue(t, out var m))
                                {
                                    m = ExtractModules(moduleCatalog, new[] {t}, knownModules).Single();
                                    knownModules.Add(t, m);
                                }
                                return m;
                            })
                        .ToArray();
                var module = new ModuleInfo(
                        moduleType,
                        moduleCatalog.GetModuleName(moduleType),
                        moduleCatalog.GetInitMethod(moduleType),
                        moduleCatalog.GetDependencyInitializedMethods(moduleType),
                        moduleCatalog.GetDependencyTerminatedMethods(moduleType),
                        moduleCatalog.GetReadyMethod(moduleType),
                        moduleCatalog.GetEntryPointMethod(moduleType),
                        requiredModules);
                yield return module;
            }
        }
    }
}