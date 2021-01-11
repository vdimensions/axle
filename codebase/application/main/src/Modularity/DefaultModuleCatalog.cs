using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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
                #if NETSTANDARD
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

        private static IList<TAttribute> CollectAttributes<TAttribute>(
                IEnumerable<Type> types, 
                IList<TAttribute> attributes, 
                bool allowInheritingAttributeTypes = false,
                bool allowAnnotatedAttributeTypes = false) 
            where TAttribute: Attribute
        {
            foreach (var type in types)
            {
                var introspector = new TypeIntrospector(type);
                var infos = introspector.GetAttributes();
                for (var i = 0; i < infos.Length; i++)
                {
                    var a = infos[i];
                    if (a.Attribute is TAttribute t)
                    {
                        if (allowInheritingAttributeTypes || t.GetType() == typeof(TAttribute)) 
                        {
                            attributes.Add(t);
                        }
                    }
                    else if (allowAnnotatedAttributeTypes)
                    {
                        CollectAttributes(new[]{a.Attribute.GetType()}, attributes, allowInheritingAttributeTypes, false);
                    }
                }
            }

            return attributes;
        }
        private const ScanOptions MemberScanOptions = ScanOptions.Instance|ScanOptions.NonPublic|ScanOptions.Public;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
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
                #if NETSTANDARD
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

        public Type GetRequiredApplicationHostType(Type moduleType)
        {
            return CollectAttributes(new[] {moduleType}, new List<ModuleAttribute>())
                .Select(ma => ma.HostableBy)
                .SingleOrDefault();
        }

        public ModuleMethod GetInitMethod(Type moduleType)
        {
            var introspector = new TypeIntrospector(moduleType);
            var method = introspector
                .GetMethods(MemberScanOptions)
                .SingleOrDefault(x => x.IsDefined<ModuleInitAttribute>(true));
            return method == null ? null : new ModuleMethod(method);
        }
        
        public ModuleMethod GetReadyMethod(Type moduleType)
        {
            var introspector = new TypeIntrospector(moduleType);
            var method = introspector
                .GetMethods(MemberScanOptions)
                .SingleOrDefault(x => x.IsDefined<ModuleReadyAttribute>(true));
            return method == null ? null : new ModuleMethod(method);
        }

        public ModuleEntryMethod GetEntryPointMethod(Type moduleType)
        {
            var introspector = new TypeIntrospector(moduleType);
            var method = introspector
                .GetMethods(MemberScanOptions)
                .SingleOrDefault(x => x.IsDefined<ModuleEntryPointAttribute>(true));
            return method == null ? null : new ModuleEntryMethod(method);
        }

        public ModuleMethod GetTerminateMethod(Type moduleType)
        {
            var introspector = new TypeIntrospector(moduleType);
            var method = introspector
                .GetMethods(MemberScanOptions)
                .SingleOrDefault(x => x.IsDefined<ModuleTerminateAttribute>(true));
            return method == null ? null : new ModuleMethod(method);
        }

        public ModuleCallback[] GetDependencyInitializedMethods(Type moduleType)
        {
            return TypeAndInterfaces(moduleType, new HashSet<Type>())
                .Select(t => new TypeIntrospector(t))
                .SelectMany(introspector => introspector
                    .GetMethods(MemberScanOptions)
                    .Select(method => 
                        new
                        {
                            Method = method, 
                            Attribute = method
                                .GetAttributes<ModuleDependencyInitializedAttribute>()
                                .Select(a => a.Attribute)
                                .Cast<ModuleDependencyInitializedAttribute>()
                                .SingleOrDefault()
                        })
                    .Where(x => x.Attribute != null)
                    .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority, m.Attribute.AllowParallelInvoke)))
                .ToArray();
        }

        public ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType)
        {
            return TypeAndInterfaces(moduleType, new HashSet<Type>())
                .Select(t => new TypeIntrospector(t))
                .SelectMany(introspector => introspector
                    .GetMethods(MemberScanOptions)
                    .Select(method => 
                        new
                        {
                            Method = method, 
                            Attribute = method
                                .GetAttributes<ModuleDependencyTerminatedAttribute>()
                                .Select(a => a.Attribute)
                                .Cast<ModuleDependencyTerminatedAttribute>()
                                .SingleOrDefault()
                        })
                    .Where(x => x.Attribute != null)
                    .Select(m => new ModuleCallback(m.Method, m.Attribute.Priority, m.Attribute.AllowParallelInvoke)))
                .ToArray();
        }

        public ModuleConfigSectionAttribute GetConfigurationInfo(Type moduleType)
        {
            return CollectAttributes(
                    TypeAndInterfaces(moduleType, new HashSet<Type>()),
                    new List<ModuleConfigSectionAttribute>())
                .LastOrDefault();
        }

        public UtilizesAttribute[] GetUtilizedModules(Type moduleType)
        {
            return CollectAttributes(
                    TypeAndInterfaces(moduleType, new HashSet<Type>()),
                    new List<UtilizesAttribute>(),
                    //
                    // Note - the `UtilizesAttribute` can be subclassed and we must take into account any derived attribute types.
                    //
                    true,
                    //
                    // Note - the `UtilizesAttribute` can be applied on another attribute. We are also scanning for them.
                    //
                    true)
                .ToArray();
        }

        public ProvidesForAttribute[] GetProvidesForModules(Type moduleType)
        {
            return CollectAttributes(
                    TypeAndInterfaces(moduleType, new HashSet<Type>()),
                    new List<ProvidesForAttribute>(),
                    //
                    // Note - the `ProvidesForAttribute` can be subclassed and we must take into account any derived attribute types.
                    //
                    true,
                    //
                    // Note - the `ProvidesForAttribute` can be applied on another attribute. We are also scanning for them.
                    //
                    true)
                .ToArray();
        }

        public ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType)
        {
            return CollectAttributes(
                    new[] { moduleType },
                    new List<ModuleCommandLineTriggerAttribute>())
                .SingleOrDefault();
        }

        public Type[] GetRequiredModules(Type moduleType)
        {
            var result = new HashSet<Type>();
            var attributes = CollectAttributes(
                TypeAndInterfaces(moduleType, new HashSet<Type>()), 
                new List<RequiresAttribute>(), 
                //
                // Note - the `RequiresAttribute` can be subclassed and we must take into account any derived attribute types.
                //
                true,
                //
                // Note - the `RequiresAttribute` can be applied on another attribute. We are also scanning for them.
                //
                true);
            for (var i = 0; i < attributes.Count; i++)
            {
                result.Add(attributes[i].ModuleType);
            }
            return result.ToArray();
        }
    }
}