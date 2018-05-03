using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Axle.Core.Infrastructure.DependencyInjection.Descriptors;
using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
{
    internal sealed class DependencyMap : IDisposable
    {
        public abstract class DependencyState
        {
            private readonly string _name;

            protected DependencyState(string name)
            {
                _name = name.VerifyArgument(nameof(name)).IsNotNull();
            }

            public abstract object Resolve();

            public abstract Type Type { get; }
            public string Name => _name;
        }

        internal sealed class ConstantDependencyState : DependencyState
        {
            private readonly object _value;

            public ConstantDependencyState(string name, object value) : base(name)
            {
                _value = value.VerifyArgument(nameof(value)).IsNotNull().Value;
            }

            public override object Resolve() => _value;

            public override Type Type => _value.GetType();
        }

        internal abstract class ConstructibleDependencyState : DependencyState
        {
            private readonly IEnumerable<ConstructionRecepie> _recepies;
            private readonly IDependencyResolver _resolver;

            protected ConstructibleDependencyState(string name, Type type, IEnumerable<ConstructionRecepie> recepie, IDependencyResolver resolver) 
                : base(name)
            {
                Type = type;
                _recepies = recepie.VerifyArgument(nameof(recepie)).IsNotNull().Value;
                _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull().Value;
            }

            public override object Resolve() => _recepies.Select(r => r.Complete(_resolver)).FirstOrDefault();

            public override Type Type { get; }
        }

        internal sealed class PrototypeDependencyState : ConstructibleDependencyState
        {
            public PrototypeDependencyState(string name, Type type, IEnumerable<ConstructionRecepie> recepies, IDependencyResolver resolver) 
                : base(name, type, recepies, resolver) { }
        }

        internal sealed class SingletonDependencyState : ConstructibleDependencyState
        {
            private object _value;

            public SingletonDependencyState(string name, Type type, IEnumerable<ConstructionRecepie> recepies, IDependencyResolver resolver) 
                : base(name, type, recepies, resolver) { }

            public override object Resolve() => _value ?? (_value = base.Resolve());
        }

        private readonly ConcurrentDictionary<string, DependencyState[]> _states;

        public DependencyMap()
        {
            _states = new ConcurrentDictionary<string, DependencyState[]>(StringComparer.Ordinal);
        }

        private void Register(string name, DependencyState state)
        {
            state.VerifyArgument(nameof(state)).IsNotNull();
            _states.AddOrUpdate(
                    name.VerifyArgument(nameof(name)),
                    x => new[] { state },
                    // TODO: repeating values for the same name should not be allowed!
                    (x, oldState) => oldState.Union(new[] { state }).ToArray());
        }

        public void RegisterConstant(string name, object value) => Register(name, new ConstantDependencyState(name, value));

        private void RegisterConstructible(
                string name, 
                Type type, 
                IDependencyDescriptorProvider ddp, 
                IDependencyResolver resolver, 
                Func<string, Type, IEnumerable<ConstructionRecepie>, IDependencyResolver, ConstructibleDependencyState> factory)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            resolver.VerifyArgument(nameof(resolver)).IsNotNull();
            ddp.VerifyArgument(nameof(ddp)).IsNotNull();
            factory.VerifyArgument(nameof(factory)).IsNotNull();

            var recepies = ConstructionRecepie.ForType(type, ddp);
            Register(name, factory(name, type, recepies, resolver));
        }

        public void RegisterSingletion(string name, Type type, IDependencyDescriptorProvider ddp, IDependencyResolver resolver)
        {
            RegisterConstructible(name, type, ddp, resolver, (a, b, c, d) => new SingletonDependencyState(a, b, c, d));
        }

        public void RegisterPrototype(string name, Type type, IDependencyDescriptorProvider ddp, IDependencyResolver resolver)
        {
            RegisterConstructible(name, type, ddp, resolver, (a, b, c, d) => new PrototypeDependencyState(a, b, c, d));
        }

        public object Resolve(string name, Type type)
        {
            if (!_states.TryGetValue(name, out var candidates))
            {
                throw new DependencyNotFoundException(type, name);
            }

            var filtered = candidates.Where(c => type.IsAssignableFrom(c.Type)).ToArray();
            switch (filtered.Length)
            {
                case 1:
                    return filtered[0].Resolve();
                case 0:
                    throw new DependencyNotFoundException(type, name);
                default:
                    // TODO: message for ambiguious candidates
                    throw new DependencyResolutionException();
            }
        }

        public void Dispose() => _states.Clear();
        void IDisposable.Dispose() => Dispose();
    }

    public class AbstractContainer : IContainer
    {
        private class ParentLookupDependencyResolver : IDependencyResolver
        {
            private readonly DependencyMap _map;
            private readonly IContainer _parentContainer;

            public ParentLookupDependencyResolver(DependencyMap map, IContainer parentContainer)
            {
                _map = map;
                _parentContainer = parentContainer;
            }

            public object Resolve(DependencyInfo dependency)
            {
                try
                {
                    if (!string.IsNullOrEmpty(dependency.DependencyName))
                    {
                        return _map.Resolve(dependency.DependencyName, dependency.Type);
                    }

                    try
                    {
                        return _map.Resolve(dependency.MemberName, dependency.Type);
                    }
                    catch (DependencyNotFoundException)
                    {
                        return _map.Resolve(string.Empty, dependency.Type);
                    }
                }
                catch (DependencyResolutionException)
                {
                    if (_parentContainer == null)
                    {
                        throw;
                    }

                    if (!string.IsNullOrEmpty(dependency.DependencyName))
                    {
                        return _parentContainer.Resolve(dependency.Type, dependency.DependencyName);
                    }

                    try
                    {
                        return _parentContainer.Resolve(dependency.Type, dependency.MemberName);
                    }
                    catch (DependencyNotFoundException)
                    {
                        return _parentContainer.Resolve(dependency.Type, string.Empty);
                    }
                }
            }
        }

        private readonly DependencyMap _dependencyMap = new DependencyMap();
        private readonly IDependencyDescriptorProvider _dependencyDescriptorProvider;

        public AbstractContainer(IContainer parent, IDependencyDescriptorProvider dependencyDescriptorProvider)
        {
            Parent = parent;
            _dependencyDescriptorProvider = dependencyDescriptorProvider;
        }

        public IContainer RegisterInstance(object instance, string name, params string[] aliases)
        {
            _dependencyMap.RegisterConstant(name, instance);
            foreach (var alias in aliases)
            {
                _dependencyMap.RegisterConstant(alias, instance);
            }
            return this;
        }

        public IContainer RegisterType(Type type, string name, params string[] aliases)
        {
            var resolver = new ParentLookupDependencyResolver(_dependencyMap, Parent);
            _dependencyMap.RegisterSingletion(name, type, _dependencyDescriptorProvider, resolver);
            foreach (var alias in aliases)
            {
                _dependencyMap.RegisterSingletion(alias, type, _dependencyDescriptorProvider, resolver);
            }
            return this;
        }

        public object Resolve(Type type, string name) => _dependencyMap.Resolve(name, type);

        public IContainer Parent { get; }
    }
}
