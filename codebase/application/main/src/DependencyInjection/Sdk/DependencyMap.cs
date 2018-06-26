#if NETSTANDARD1_3_OR_NEWER || !NETSTANDARD
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Application.DependencyInjection.Sdk
{
    internal sealed class DependencyMap : IDisposable
    {
        internal abstract class DependencyState
        {
            protected DependencyState(string name)
            {
                Name = name.VerifyArgument(nameof(name)).IsNotNull();
            }

            public abstract object Resolve();

            public abstract Type Type { get; }
            public string Name { get; }
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

            protected ConstructibleDependencyState(string name, Type type, IEnumerable<ConstructionRecepie> recepies, IDependencyResolver resolver) 
                : base(name)
            {
                Type = type;
                _recepies = recepies.VerifyArgument(nameof(recepies)).IsNotNull().Value;
                _resolver = resolver.VerifyArgument(nameof(resolver)).IsNotNull().Value;
            }

            public override object Resolve() => _recepies.Select(r => r.Complete(_resolver)).FirstOrDefault();

            public sealed override Type Type { get; }
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

        /// <summary>
        /// Creates a new instance of the <see cref="DependencyMap"/> class.
        /// </summary>
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
                    (x, oldState) =>
                    {
                        if (oldState.Any(os => state.Type == os.Type))
                        {
                            // Repeating type values for the same name should not be allowed!
                            throw new DuplicateDependencyDefinitionException(state.Type, name);
                        }
                        return oldState.Union(new[] {state}).ToArray();
                    });
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
                // TODO: assume prototyping
                throw new DependencyNotFoundException(type, name);
            }
            #if NETSTANDARD || NET45_OR_NEWER
            var filtered = candidates.Where(c => type.GetTypeInfo().IsAssignableFrom(c.Type.GetTypeInfo())).ToArray();
            #else
            var filtered = candidates.Where(c => type.IsAssignableFrom(c.Type)).ToArray();
            #endif
            switch (filtered.Length)
            {
                case 1:
                    return filtered[0].Resolve();
                case 0:
                    throw new DependencyNotFoundException(type, name);
                default:
                    throw new AmbiguousDependencyException(type, name, filtered.Select(x => new DependencyCandidate(x.Type, x.Name)));
            }
        }

        public void Dispose() => _states.Clear();
        void IDisposable.Dispose() => Dispose();
    }
}
#endif