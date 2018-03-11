//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//
//using Axle.Application.Aop;
//using Axle.Application.Configuration;
//using Axle.Application.IoC.Sdk;
//using Axle.Configuration;
//using Axle.Core;
//using Axle.Core.Infrastructure.DependencyInjection;
//using Axle.Core.Infrastructure.DependencyInjection.Impl;
//using Axle.Extensions.String;
//using Axle.Extensions.Type;
//using Axle.Linq;
//using Axle.Verification;
//
//
//namespace Axle.Application.IoC.Impl
//{
//    using ResolvableOrException = Union<IResolvable, Exception>;
//
//    internal sealed class DefaultKernel : MarshalByRefObject, IKernel
//    {
//        private static IProxyGenerator GetProxyGenerator()
//        {
//            var appSection = ConfigurationReader.Instance.CurrentConfiguration.GetSection<IApplicationConfigurationSection>();
//            Type configuredProxyGenerator = null;
//            if (appSection != null)
//            {
//                // Force loading if module assemblies if not loaded.
//                appSection.Modularity.ModuleDiscovery.IncludedAssemblies.Any();
//                configuredProxyGenerator = appSection.ProxyGeneratorImplementation;
//            }
//            var type = configuredProxyGenerator ?? AppDomain.CurrentDomain.GetAssemblies()
//                .SelectMany(x => x.GetTypes().Where(t => t.IsPublic && !t.IsInterface && !t.IsAbstract && t.ExtendsOrImplements<IProxyGenerator>()))
//                .OnlyOrDefault();
//            if (type == null)
//            {
//                throw new InvalidOperationException("Could not find an implementation of {0} in the class path.".FormatFor(typeof (IProxyGenerator).FullName));
//            }
//            return (IProxyGenerator) Activator.CreateInstance(type);
//        }
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly DefaultKernel parent;
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly DefaultKernel root;
//
//        private readonly IProxyManager proxyManager;
//        private readonly IDependencyResolutionErrorHandler dependencyResolutionErrorHandler;
//
//        #if !DEBUG
//        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
//        #endif
//        private readonly DependencyPool pool;
//
//        public DefaultKernel() : this(null) { }
//        public DefaultKernel(IKernel parent) : this((DefaultKernel) parent) { }
//        private DefaultKernel(DefaultKernel parent) : this(parent, parent == null ? null : parent.root) { }
//        private DefaultKernel(DefaultKernel parent, DefaultKernel root)
//        {
//            var chain = new DependencyResolutionErrorHandlerChain();
//            if (parent == null)
//            {
//                this.pool = new DependencyPool(this.proxyManager = new ProxyManager(GetProxyGenerator()));
//                this.pool.RegisterInstance(this.proxyManager, string.Empty);
//                this.pool.RegisterInstance(this.dependencyResolutionErrorHandler = chain, string.Empty);
//            }
//            else
//            {
//                this.pool = new DependencyPool(this.proxyManager = parent.proxyManager);
//                this.pool.RegisterInstance(this.dependencyResolutionErrorHandler = chain, string.Empty);
//                this.parent = parent;
//                chain.Push(parent.dependencyResolutionErrorHandler);
//            }
//            this.root = root ?? this;
//        }
//
//        void IDisposable.Dispose()
//        {
//            if (pool != null)
//            {
//                var p = pool;
//                p.Dispose();
//            }
//        }
//
//        public IEnumerable<IDependencyReference> GetDependencies()
//        {
//            var result = parent == null
//                ? pool.GetDependencies()
//                : pool.GetDependencies().Union(parent.GetDependencies()).Distinct(new DependencyReferenceEqualityComparer());
//            return result;
//        }
//        public IEnumerable<IDependencyReference> GetDependencies(Type type)
//        {
//            var result = parent == null 
//                ? pool.GetDependencies(type) 
//                : pool.GetDependencies(type).Union(parent.GetDependencies(type)).Distinct(new DependencyReferenceEqualityComparer());
//            return result;
//        }
//
//        public void RegisterSingleton(Type type, string name, params string[] aliases)
//        {
//            pool.RegisterSingleton(
//                type.VerifyArgument("type")
//                    .IsNotNull()
//                    .IsTrue(x => !x.IsInterface, string.Format("Unable to register type {0} as dependency. Type cannot be an interface.", type.FullName))
//                    .IsTrue(x => !x.IsAbstract,  string.Format("Unable to register type {0} as dependency. Type cannot be an abstract class.", type.FullName))
//                    .IsTrue(x => !x.IsValueType, string.Format("Unable to register type {0} as dependency. Type cannot be a struct.", type.FullName))
//                    .Value,
//                name.VerifyArgument("name").IsNotNull(),
//                aliases);
//        }
//
//        public void RegisterPrototype(Type type, string name, params string[] aliases)
//        {
//            pool.RegisterPrototype(
//                type.VerifyArgument("type")
//                    .IsNotNull()
//                    .IsTrue(x => !x.IsInterface, string.Format("Unable to register type {0} as dependency. Type cannot be an interface.", type.FullName))
//                    .IsTrue(x => !x.IsAbstract, string.Format("Unable to register type {0} as dependency. Type cannot be an abstract class.", type.FullName))
//                    .IsTrue(x => !x.IsValueType, string.Format("Unable to register type {0} as dependency. Type cannot be a struct.", type.FullName))
//                    .Value,
//                name.VerifyArgument("name").IsNotNull(),
//                aliases);
//        }
//
//        public void RegisterInstance(object instance, string name, params string[] aliases)
//        {
//            pool.RegisterInstance(
//                instance.VerifyArgument("instance").IsNotNull().Value, 
//                name.VerifyArgument("name").IsNotNull(),
//                aliases);
//        }
//
//        private bool TryGet(Container container, IDependency dependency, bool inferName, out DependencyResolutionException ex, out IResolvable resolvable)
//        {
//            var type = dependency.RequestedType;
//            var name = inferName ? dependency.InferredName : dependency.DeclaredName;
//            try
//            {
//                if (pool.TryGet(container, dependency, inferName, dependencyResolutionErrorHandler, out resolvable))
//                {
//                    ex = null;
//                    return true;
//                }
//            }
//            catch (DependencyResolutionException e)
//            {
//                ex = e;
//                resolvable = null;
//                return false;
//            }
//            catch (Exception e)
//            {
//                ex = new DependencyResolutionException(type, name, e);
//                resolvable = null;
//                return false;
//            }
//            ex = null;
//            return false;
//        }
//
//        public bool TryResolve(Container container, Type type, string name, out object instance, out DependencyResolutionException ex)
//        {
//            IResolvable resolvable;
//            var resullt = TryResolve(
//                container,
//                new Dependency(type, name),
//                false,
//                null,
//                parent,
//                new HashSet<IDependency>(new DependencyEqualityComparer()),
//                out resolvable,
//                out ex);
//            if (resullt && resolvable.IsResolved)
//            {
//                instance = resolvable.Value;
//                return true;
//            }
//            ex = ex ?? new DependencyResolutionException(type, name, null);
//            instance = null;
//            return false;
//        }
//
//        private bool TryResolve(
//            Container container, 
//            IDependency dependency,
//            bool inferName,
//            IResolvable targetResolvable,
//            DefaultKernel backwardLookup,
//            HashSet<IDependency> recursionPreventionSet,
//            out IResolvable resolvable,
//            out DependencyResolutionException exception)
//        {
//            var name = inferName ? dependency.InferredName : dependency.DeclaredName;
//            if (!recursionPreventionSet.Add(dependency))
//            {
//                exception = new DependencyResolutionException(
//                    dependency.RequestedType,
//                    dependency.DeclaredName, 
//                    string.Format(
//                        "A circular dependency exists when resolving an instance {0}of type {1}",
//                        string.IsNullOrEmpty(name) ? string.Empty : string.Format(" for name '{0}' ", name),
//                        dependency.RequestedType.FullName), 
//                    null);
//                resolvable = null;
//                return false;
//            }
//            var foundResolvable = TryGet(container, dependency, inferName, out exception, out resolvable);
//            var isRecursiveSelfResolve = (targetResolvable != null) && (resolvable != null) && (targetResolvable.Type == resolvable.Type);
//            if (!foundResolvable || isRecursiveSelfResolve)
//            {
//                resolvable = null;
//                if ((backwardLookup != null) && ((exception == null) || exception.IsRecoverable))
//                {
//                    recursionPreventionSet.Remove(dependency);
//                    DependencyResolutionException backwardException;
//                    if (backwardLookup.TryResolve(
//                        container,
//                        dependency, 
//                        inferName,
//                        targetResolvable,
//                        backwardLookup.parent, 
//                        recursionPreventionSet, 
//                        out resolvable, 
//                        out backwardException))
//                    {
//                        exception = resolvable.IsResolved ? null : CreateNotFullyResolvedException(dependency.RequestedType, name, backwardException);
//                        if (resolvable.IsInjectable)
//                        {
//                            recursionPreventionSet.Remove(dependency);
//                        }
//                        return true;
//                    }
//                    exception = new DependencyResolutionException(dependency.RequestedType, name, backwardException ?? exception);
//                    resolvable = null;
//                }
//
//                return false;
//            }
//            if (resolvable == null)
//            {
//                exception = new DependencyNotFoundException(dependency.RequestedType, name, dependency, exception);
//                return false;
//            }
//
//            if (resolvable.IsInjectable)
//            {
//                exception = resolvable.IsResolved ? null : CreateNotFullyResolvedException(dependency.RequestedType, name, null);
//                recursionPreventionSet.Remove(dependency);
//                return true;
//            }
//
//            DependencyResolutionException dependencyException;
//            if (SatisfyDependencies(container, dependency.RequestedType, name, recursionPreventionSet, backwardLookup, ref resolvable, out dependencyException))
//            {
//                exception = resolvable.IsResolved ? null : CreateNotFullyResolvedException(dependency.RequestedType, name, dependencyException);
//                if (resolvable.IsResolved)
//                {
//                    recursionPreventionSet.Remove(dependency);
//                }
//                return true;
//            }
//            exception = new DependencyResolutionException(dependency.RequestedType, name, null);
//            return false;
//        }
//
//        private bool SatisfyDependencies(
//            Container container, 
//            Type type, 
//            string name,
//            HashSet<IDependency> recursionPreventionSet,
//            DefaultKernel backwardLookup,
//            ref IResolvable resolvable,
//            out DependencyResolutionException dependencyException)
//        {
//            if (resolvable.IsInjectable)
//            {
//                dependencyException = null;
//                return true;
//            }
//
//            dependencyException = null;
//            foreach (var recepy in resolvable.Recepies)
//            {
//                IEnumerable<object> args;
//                if (!GatherDependencies(
//                    container,
//                    type, 
//                    name, 
//                    resolvable,
//                    recepy.FactoryParameters, 
//                    recursionPreventionSet, 
//                    backwardLookup, 
//                    x => x.IsInjectable,
//                    (_, x) => x, 
//                    out dependencyException, 
//                    out args))
//                {
//                    continue;
//                }
//                if (args.Count() != recepy.FactoryParameters.Length)
//                {
//                    dependencyException = new DependencyResolutionException(type, name, "No suitable constructor or factory method found.", null);
//                    continue;
//                }
//
//                object instance;
//                try
//                {
//                    instance = recepy.Cook(args.ToArray());
//                }
//                catch (Exception e)
//                {
//                    dependencyException = new DependencyResolutionException(type, name, "An error occurred in constructor / factory method. Check inner exception for details.", e);
//                    return false;
//                }
//                resolvable.MarkInjectable(instance);
//
//                IEnumerable<KeyValuePair<IPropertyDependency, object>> result;
//                if (!GatherDependencies(
//                    container,
//                    type, 
//                    name,
//                    resolvable,
//                    recepy.Properties, 
//                    recursionPreventionSet, 
//                    backwardLookup,
//                    x => x.IsInjectable,
//                    (x, y) => new KeyValuePair<IPropertyDependency, object>(x, y), 
//                    out dependencyException, 
//                    out result))
//                {
//                    return false;
//                }
//                foreach (var keyValuePair in result)
//                {
//                    try
//                    {
//                        keyValuePair.Key.Set(instance, keyValuePair.Value);
//                    }
//                    catch (Exception e)
//                    {
//                        dependencyException = new DependencyResolutionException(
//                            type, 
//                            name, 
//                            string.Format("An error setting property '{0}'. Check inner exception for details.", keyValuePair.Key.PropertyName), 
//                            e);
//                        return false;
//                    }
//                }
//                // TODO: call any init methods
//                resolvable.MarkResolved();
//                return true;
//            }
//            dependencyException = new DependencyResolutionException(type, name, dependencyException);
//            return false;
//        }
//
//        private bool GatherDependencies<TD, T>(
//            Container container, 
//            Type type,
//            string name,
//            IResolvable targetResolvable,
//            IEnumerable<TD> dependencies,
//            HashSet<IDependency> rps,
//            DefaultKernel backwardLookup,
//            Predicate<IResolvable> accepts,
//            Func<TD, object, T> func,
//            out DependencyResolutionException dependencyException,
//            out IEnumerable<T> result) where TD : IDependency
//        {
//            var args = new List<T>();
//            foreach (var dependency in dependencies)
//            {
//                var forceName = !string.IsNullOrEmpty(dependency.DeclaredName);
//                IResolvable dependencyResolvable;
//                var failed = false;
//                if (!TryResolve(container, dependency, !forceName, targetResolvable, backwardLookup, new HashSet<IDependency>(rps), out dependencyResolvable, out dependencyException))
//                {
//                    if (!forceName)
//                    {
//                        if (!TryResolve(container, dependency, false, targetResolvable, backwardLookup, new HashSet<IDependency>(rps), out dependencyResolvable, out dependencyException))
//                        {
//                            failed = true;
//                        }
//                    }
//                    else
//                    {
//                        failed = true;
//                    }
//                }
//
//                if (failed && dependency.IsOptional)
//                {
//                    continue;
//                }
//
//                if (failed || !accepts(dependencyResolvable))
//                {
//                    if (dependencyResolvable != null)
//                    {
//                        dependencyException = new DependencyNotFoundException(type, name, dependency, dependencyException);
//                    }
//                    // TODO: check binding type options here
//                    result = null;
//                    return false;
//                }
//                args.Add(func(dependency, dependencyResolvable.Value));
//            }
//            dependencyException = null;
//            result = args;
//            return true;
//        }
//
//        private static DependencyResolutionException CreateNotFullyResolvedException(Type type, string name, Exception innerException)
//        {
//            var exception = new DependencyResolutionException(
//                string.Format("Dependency {0}of type {1} cannot be completely resolved", name == null ? string.Empty : string.Format("'{0}' ", name), type.FullName),
//                innerException);
//            return exception;
//        }
//
//        public IProxyManager ProxyManager { get { return proxyManager; } }
//        public IKernel Parent { get { return parent; } }
//        public IKernel Root { get { return root; } }
//    }
//}