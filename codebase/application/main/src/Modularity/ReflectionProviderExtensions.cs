using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Verification;


namespace Axle.Application.Modularity
{
    public static class ReflectionProviderExtensions
    {
        public static ModuleInfo[] GetModules(this IReflectionProvider reflectionProvider)
        {
            reflectionProvider.VerifyArgument(nameof(reflectionProvider)).IsNotNull();
            return ExtractModules(reflectionProvider, reflectionProvider.DiscoverModuleTypes(), new Dictionary<Type, ModuleInfo>()).ToArray();
        }

        private static IEnumerable<ModuleInfo> ExtractModules(
                IReflectionProvider reflectionProvider, 
                IEnumerable<Type> types, 
                IDictionary<Type, ModuleInfo> knownModules)
        {
            foreach (var type in types)
            {
                var requiredModuleTypes = reflectionProvider.GetRequiredModules(type);
                var requiredModules = requiredModuleTypes
                        .Select(
                            t =>
                            {
                                if (!knownModules.TryGetValue(t, out var m))
                                {
                                    m = ExtractModules(reflectionProvider, new[] {t}, knownModules).Single();
                                    knownModules.Add(t, m);
                                }
                                return m;
                            })
                        .ToArray();
                var module = new ModuleInfo(
                        type,
                        reflectionProvider.GetModuleName(type),
                        reflectionProvider.GetInitMethod(type),
                        reflectionProvider.GetDependencyInitializedMethods(type),
                        reflectionProvider.GetDependenciesReadyMethodMethod(type),
                        reflectionProvider.GetEntryPointMethod(type),
                        requiredModules);
                yield return module;
            }
        }
    }
}