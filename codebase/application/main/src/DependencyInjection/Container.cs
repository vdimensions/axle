#if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
using System;

using Axle.DependencyInjection.Sdk;


namespace Axle.DependencyInjection
{
    public class Container : AbstractContainer, IDisposable
    {
        public Container(IContainer parent) : base(parent)
        {
            DependencyDescriptorProvider = new DefaultDependencyDescriptorProvider();
        }

        protected override IDependencyDescriptorProvider DependencyDescriptorProvider { get; }
    }
}
#endif