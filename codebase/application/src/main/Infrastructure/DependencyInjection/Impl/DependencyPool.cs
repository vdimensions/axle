//#if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//#if NETSTANDARD || NET45_OR_NEWER
//using System.Reflection;
//#endif
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    internal sealed class DependencyPool
//    {
//        private static IEnumerable<IDependencyReference> GetDependencies(IEnumerable<KeyValuePair<Type, ConcurrentDictionary<string, Resolvable>>> d)
//        {
//            #warning operations below jeopardize concurrent access effectiveness
//            return d
//                .SelectMany(x => x.Value.Select(y => new { Type = x.Key, Name = y.Key, Data = y.Value }))
//                .Select(y => new DependencyReference(y.Type, y.Name, y.Data as IResolvable)) //TODO: remove cast
//                .ToArray();
//        }
//
////        private readonly IProxyManager proxyManager;
//        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, Resolvable>> data = new ConcurrentDictionary<Type, ConcurrentDictionary<string, Resolvable>>();
////
////        public DependencyPool(IProxyManager proxyManager)
////        {
////            this.proxyManager = proxyManager;
////        }
////
//        public void Dispose() { data.Clear(); }
////
//        public IEnumerable<IDependencyReference> GetDependencies() => GetDependencies(data);
//        public IEnumerable<IDependencyReference> GetDependencies(Type type)
//        {
//            #if NETSTANDARD || NET45_OR_NEWER
//            return GetDependencies(data.Where(x => type.GetTypeInfo().IsAssignableFrom(x.Key.GetTypeInfo())));
//            #else
//            return GetDependencies(data.Where(x => type.IsAssignableFrom(x.Key)));
//            #endif
//        }
//
//        public void RegisterSingleton(Type type, string name, params string[] aliases) => Register(type, name, true, aliases);
//
//        public void RegisterPrototype(Type type, string name, params string[] aliases) => Register(type, name, false, aliases);
//
//        private void Register(Type type, string name, bool asSingleton, params string[] aliases)
//        {
//            var comparer = StringComparer.Ordinal;
//            var dict = data.GetOrAdd(type, t => new ConcurrentDictionary<string, Resolvable>(StringComparer.Ordinal));
//            var item = null as Resolvable;//new Resolvable(type, proxyManager, asSingleton); // TODO
//            if (!dict.TryAdd(name, item))
//            {
//                throw new DuplicateDependencyDefinitionException(
//                    string.Format("Dependency {0}of type {1} is already registered.", name == null ? string.Empty : $"'{name}' ", name));
//            }
//            foreach (var alias in aliases.Except(new[]{string.Empty, name}, comparer))
//            {
//                if (!dict.TryAdd(alias, item))
//                {
//                    throw new DuplicateDependencyDefinitionException(string.Format("A dependency '{0}' of type {1} is already registered.", alias, alias));
//                }
//            }
//        }
////
////        public void RegisterInstance(object instance, string name, params string[] aliases)
////        {
////            var comparer = StringComparer.Ordinal;
////            var type = instance.GetType();
////            var dict = data.GetOrAdd(type, t => new ConcurrentDictionary<string, Resolvable>(StringComparer.Ordinal));
////            var resolvable = new Resolvable(type, proxyManager, true);
////            resolvable.MarkInjectable(instance);
////            resolvable.MarkResolved();
////            if (!dict.TryAdd(name, resolvable))
////            {
////                throw new DuplicateDependencyDefinitionException(
////                    string.Format(
////                        "Dependency {0}of type {1} is already registered.",
////                        name == null ? string.Empty : string.Format("'{0}' ", name),
////                        instance.GetType()));
////            }
////            foreach (var alias in aliases.Except(new[] { string.Empty, name }, comparer))
////            {
////                if (!dict.TryAdd(alias, resolvable))
////                {
////                    throw new DuplicateDependencyDefinitionException(string.Format("Dependency '{0}' of type {1} is already registered.", alias, alias));
////                }
////            }
////        }
////
////        public bool TryGet(Container container, IDependency dependency, bool inferName, IDependencyResolutionErrorHandler handler, out IResolvable resolvable)
////        {
////            var type = dependency.RequestedType;
////            var name = inferName ? dependency.InferredName : dependency.DeclaredName;
////            var resolvables = data
////                .Where(x => type.IsAssignableFrom(x.Key))
////                .Select(
////                    x =>
////                    {
////                        Resolvable res;
////                        return x.Value.TryGetValue(name, out res) ? res : null;
////                    })
////                .Where(x => x != null)
////                .ToArray();
////            if (resolvables.Length == 0)
////            {
////                var resolution = inferName 
////                    ? handler.ResolveMissingInferredDependency(container, dependency)
////                    : handler.ResolveMissingDeclaredDependency(container, dependency);
////                if (resolution.Succeeded)
////                {
////                    resolvable = RegisterResolvable(name, resolution);
////                    return true;
////                }
////                resolvable = null;
////                return false;
////            }
////            if (resolvables.Length > 1)
////            {
////                var resolution = inferName
////                    ? handler.ResolveMultipleCandidatesInferredDependency(container, dependency, resolvables)
////                    : handler.ResolveMultipleCandidatesDeclaredDependency(container, dependency, resolvables);
////                if (resolution.Succeeded)
////                {
////                    resolvable = RegisterResolvable(name, resolution);
////                    return true;
////                }
////                throw new DependencyResolutionException(
////                    string.Format(
////                        "Cannot resolve type {0}. Multiple matching candidates have been found within the current container.", 
////                        type.FullName), 
////                        false);
////            }
////            var r = resolvables[0];
////            resolvable = r.IsSingleton ? r : new Resolvable(r.Type, proxyManager);
////            return true;
////        }
////
////        private Resolvable RegisterResolvable(string name, IDependencyResolution resolution)
////        {
////            return data
////                .GetOrAdd(resolution.Type, new ConcurrentDictionary<string, Resolvable>())
////                .GetOrAdd(
////                    name, 
////                    x =>
////                    {
////                        var r = new Resolvable(resolution.Type, proxyManager, resolution.IsSingleton);
////                        r.MarkInjectable(resolution.Value);
////                        r.MarkResolved();
////                        return r;
////                    });
////        }
//    }
//}
//#endif