#if NETSTANDARD1_3_OR_NEWER || !NETSTANDARD
using System;


namespace Axle.Application.DependencyInjection.Sdk
{
    public abstract partial class AbstractContainer : IContainer
    {
        private readonly DependencyMap _dependencyMap = new DependencyMap();

        protected AbstractContainer(IContainer parent)
        {
            Parent = parent;
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
            _dependencyMap.RegisterSingletion(name, type, DependencyDescriptorProvider, resolver);
            foreach (var alias in aliases)
            {
                _dependencyMap.RegisterSingletion(alias, type, DependencyDescriptorProvider, resolver);
            }
            return this;
        }

        public object Resolve(Type type, string name) => _dependencyMap.Resolve(name, type);

        public IContainer Parent { get; }
        protected abstract IDependencyDescriptorProvider DependencyDescriptorProvider { get; }
    }
}
#endif