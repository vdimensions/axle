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

        private static IEnumerable<ModuleInfo> ExtractModules(
                IModuleCatalog moduleCatalog, 
                IEnumerable<Type> types, 
                IDictionary<Type, ModuleInfo> knownModules)
        {
            foreach (var type in types)
            {
                var requiredModuleTypes = moduleCatalog.GetRequiredModules(type);
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
                        type,
                        moduleCatalog.GetModuleName(type),
                        moduleCatalog.GetInitMethod(type),
                        moduleCatalog.GetDependencyInitializedMethods(type),
                        moduleCatalog.GetDependencyTerminatedMethods(type),
                        moduleCatalog.GetReadyMethod(type),
                        moduleCatalog.GetEntryPointMethod(type),
                        requiredModules);
                yield return module;
            }
        }
    }
}