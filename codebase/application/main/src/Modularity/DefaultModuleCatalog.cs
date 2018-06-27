using System;
using System.Linq;

using Axle.Environment;
using Axle.Reflection;


namespace Axle.Application.Modularity
{
    internal sealed class DefaultModuleCatalog : IModuleCatalog
    {
        private ScanOptions memberScanOptions = ScanOptions.Instance|ScanOptions.NonPublic|ScanOptions.Public;

        public Type[] DiscoverModuleTypes()
        {
            return Platform.Runtime.GetAssemblies()
                                   .SelectMany(x => x.GetTypes())
                                   #if NETSTANDARD1_5
                                   .Select(t => t.GetTypeInfo())
                                   #endif
                                   .Where(t => !t.IsAbstract && !t.IsInterface)
                                   .Where(t => t.GetCustomAttributes(typeof(ModuleInitAttribute), false).Any())
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
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleInitAttribute));
            return new ModuleMethod(m);
        }

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            return introspector.GetMethods(memberScanOptions)
                                .Select(x => new { Method = x, Attribute = x.Attributes.Select(a => a.Attribute as ModuleDependencyInitializedAttribute).SingleOrDefault() })
                                .Where(x => x.Attribute != null)
                                .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority))
                                .ToArray();
        }

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            return introspector.GetMethods(memberScanOptions)
                .Select(x => new { Method = x, Attribute = x.Attributes.Select(a => a.Attribute as ModuleDependencyTerminatedAttribute).SingleOrDefault() })
                .Where(x => x.Attribute != null)
                .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority))
                .ToArray();
        }

        public ModuleMethod GetReadyMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(memberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleReadyAttribute));
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
            return introspector.GetAttributes(typeof(RequiresAttribute))
                               .Select(x => ((RequiresAttribute) x.Attribute).ModuleType)
                               .ToArray();
        }
    }
}