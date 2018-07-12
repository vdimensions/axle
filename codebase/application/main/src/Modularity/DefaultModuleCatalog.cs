using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
using Axle.Environment;
#endif
using Axle.Reflection;
using Axle.Verification;


namespace Axle.Modularity
{
    internal sealed class DefaultModuleCatalog : IModuleCatalog
    {
        private static IEnumerable<Type> TypeAndInterfaces(Type type, HashSet<Type> types)
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

        private static IList<TAttribute> CollectAttributes<TAttribute>(IEnumerable<Type> types, IList<TAttribute> attributes) where TAttribute: Attribute
        {
            foreach (var type in types)
            {
                var introspector = new DefaultIntrospector(type);
                var introspectedAttrobutes = introspector.GetAttributes(typeof(TAttribute));
                for (var i = 0; i < introspectedAttrobutes.Length; i++)
                {
                    attributes.Add((TAttribute) introspectedAttrobutes[i].Attribute);
                }
            }

            return attributes;
        }
        private const ScanOptions MemberScanOptions = ScanOptions.Instance|ScanOptions.NonPublic|ScanOptions.Public;

        #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
        public Type[] DiscoverModuleTypes()
        {
            return Platform.Runtime.GetAssemblies().SelectMany(DiscoverModuleTypes).ToArray();
        }
        #endif

        public Type[] DiscoverModuleTypes(Assembly assembly)
        {
            assembly.VerifyArgument(nameof(assembly)).IsNotNull();

            var result = new List<Type>();
            var types = assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var t = types[i].GetTypeInfo();
                #else
                var t = types[i];
                #endif

                if (t.IsInterface || t.IsAbstract)
                {
                    continue;
                }

                var attributes = CollectAttributes(TypeAndInterfaces(types[i], new HashSet<Type>()), new List<ModuleAttribute>());
                if (attributes.Count == 0)
                {
                    continue;
                }

                result.Add(types[i]);
            }

            return result.ToArray();
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
                        .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority, m.Attribute.AllowParallelInvoke)))
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
                        .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority, m.Attribute.AllowParallelInvoke)))
                   .ToArray();
        }

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(MemberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleEntryPointAttribute));
            return m == null ? null : new ModuleEntryMethod(m);
        }

        public ModuleMethod GetTerminateMethod(Type moduleType)
        {
            var introspector = new DefaultIntrospector(moduleType);
            var m = introspector.GetMethods(MemberScanOptions)
                                .SingleOrDefault(x => x.Attributes.Any(a => a.Attribute is ModuleTerminateAttribute));
            return m == null ? null : new ModuleMethod(m);
        }

        public Type[] GetRequiredModules(Type moduleType)
        {
            var result = new HashSet<Type>();
            var attributes = CollectAttributes(TypeAndInterfaces(moduleType, new HashSet<Type>()), new List<RequiresAttribute>());
            for (var i = 0; i < attributes.Count; i++)
            {
                result.Add(attributes[i].ModuleType);
            }
            return result.ToArray();
        }
    }
}