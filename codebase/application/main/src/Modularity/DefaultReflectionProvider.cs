using System;
using System.Linq;

using Axle.Environment;
using Axle.Reflection;


namespace Axle.Application.Modularity
{
    public sealed class DefaultReflectionProvider : IReflectionProvider
    {
        private ScanOptions memberScanOptions = ScanOptions.Instance|ScanOptions.NonPublic|ScanOptions.Public;

        public Type[] DiscoverModuleTypes()
        {
            return Platform.Runtime.GetAssemblies()
                                   .SelectMany(x => x.GetTypes())
                                    #if NETSTANDARD1_5
                                   .Where(t => t.GetTypeInfo().GetCustomAttributes(typeof(ModuleInitMethodAttribute), false).Any())
                                    #else
                                   .Where(t => !t.IsAbstract && !t.IsInterface)
                                   .Where(t => t.GetCustomAttributes(typeof(ModuleInitMethodAttribute), false).Any())
                                    #endif
                                   .ToArray();
        }

        public string GetModuleName(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            return introspector.GetAttributes(typeof(ModuleAttribute))
                               .Select(x => ((ModuleAttribute) x.Attribute).Name)
                               .SingleOrDefault() ?? moduleType.FullName;
        }

        public ModuleMethod GetInitMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(memberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleInitMethodAttribute));
            return new ModuleMethod(m);
        }

        public ModuleNotifyMethod[] GetDependencyInitializedMethods(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            return introspector.GetMethods(memberScanOptions)
                                .Where(x => x.Attributes.Any(a => a.Attribute is ModuleOnDependencyInitializedAttribute))
                                .Select(m => new ModuleNotifyMethod(m))
                               .ToArray();
        }

        public ModuleMethod GetDependenciesReadyMethodMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(memberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleOnDependenciesReadyAttribute));
            return new ModuleMethod(m);
        }

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(memberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleEntryPointAttribute));
            return new ModuleEntryMethod(m);
        }

        public Type[] GetRequiredModules(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            return introspector.GetAttributes(typeof(DependsOnModuleAttribute))
                               .Select(x => ((DependsOnModuleAttribute) x.Attribute).ModuleType)
                               .ToArray();
        }
    }
}