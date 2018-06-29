using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using Axle.Environment;
#endif
using Axle.Reflection;


namespace Axle.Application.Modularity
{
    internal sealed class DefaultModuleCatalog : IModuleCatalog
    {
        private const ScanOptions MemberScanOptions = ScanOptions.Instance|ScanOptions.NonPublic|ScanOptions.Public;

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
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
        #endif

        public string GetModuleName(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var name = introspector.GetAttributes(typeof(ModuleAttribute))
                                   .Select(x => ((ModuleAttribute) x.Attribute).Name)
                                   .SingleOrDefault() ?? string.Empty;
            return string.IsNullOrEmpty(name) ? moduleType.FullName : name;
        }

        public ModuleMethod GetInitMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(MemberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleInitAttribute));
            return m == null ? null : new ModuleMethod(m);
        }

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType)
        {
            var introspectors = TypeAndInterfaces(moduleType, new HashSet<Type>()).Select(t => new DefaultIntrospector(t));
            return introspectors
                   .SelectMany(i =>
                       i.GetMethods(MemberScanOptions)
                        .Select(x => new { Method = x, Attribute = x.Attributes.Select(a => a.Attribute as ModuleDependencyInitializedAttribute).SingleOrDefault(a => a != null) })
                        .Where(x => x.Attribute != null)
                        .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority)))
                   .ToArray();
        }

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType)
        {
            var introspectors = TypeAndInterfaces(moduleType, new HashSet<Type>()).Select(t => new DefaultIntrospector(t));
            return introspectors
                   .SelectMany(i => 
                       i.GetMethods(MemberScanOptions)
                        .Select(x => new { Method = x, Attribute = x.Attributes.Select(a => a.Attribute as ModuleDependencyTerminatedAttribute).SingleOrDefault(a => a != null) })
                        .Where(x => x.Attribute != null)
                        .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority)))
                   .ToArray();
        }

        public ModuleMethod GetReadyMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(MemberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleReadyAttribute));
            return m == null ? null : new ModuleMethod(m);
        }

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(MemberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleEntryPointAttribute));
            return m == null ? null : new ModuleEntryMethod(m);
        }

        public Type[] GetRequiredModules(Type moduleType)
        {
            var introspectors = TypeAndInterfaces(moduleType, new HashSet<Type>()).Select(t => new DefaultIntrospector(t));
            return introspectors.SelectMany(i => i.GetAttributes(typeof(RequiresAttribute)).Select(x => ((RequiresAttribute) x.Attribute).ModuleType))
                                .ToArray();
        }

        private IEnumerable<Type> TypeAndInterfaces(Type type, HashSet<Type> types)
        {
            if (types.Add(type))
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var interfaces = type.GetTypeInfo().GetInterfaces();
                #else
                var interfaces = type.GetInterfaces();
                #endif
                for (var i = 0; i < interfaces.Length; i++)
                {
                    TypeAndInterfaces(interfaces[i], types);
                }
            }
            return types;
        }
    }
}