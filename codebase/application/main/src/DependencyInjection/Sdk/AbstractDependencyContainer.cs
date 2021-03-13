﻿#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractDependencyContainer : IDependencyContainer
    {
        private readonly DependencyMap _dependencyMap = new DependencyMap();

        protected AbstractDependencyContainer(IDependencyContext parent)
        {
            Parent = parent;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
        }

        public void Dispose() => Dispose(true);
        void IDisposable.Dispose() => Dispose(true);

        public IDependencyContainer RegisterInstance(object instance, string name, params string[] aliases)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            _dependencyMap.RegisterConstant(name, instance);
            foreach (var alias in aliases)
            {
                _dependencyMap.RegisterConstant(alias, instance);
            }
            return this;
        }

        public IDependencyContainer RegisterType(Type type, string name, params string[] aliases)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            var resolver = new ParentLookupDependencyResolver(_dependencyMap, Parent);
            _dependencyMap.RegisterSingletion(name, type, DependencyDescriptorProvider, resolver);
            foreach (var alias in aliases)
            {
                _dependencyMap.RegisterSingletion(alias, type, DependencyDescriptorProvider, resolver);
            }
            return this;
        }

        public object Resolve(Type type, string name) => _dependencyMap.Resolve(name, type);

        public IDependencyContext Parent { get; }
        protected abstract IDependencyDescriptorProvider DependencyDescriptorProvider { get; }
    }
}
#endif