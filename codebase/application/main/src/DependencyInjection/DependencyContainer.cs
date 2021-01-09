#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;

using Axle.DependencyInjection.Sdk;


namespace Axle.DependencyInjection
{
    public class DependencyContainer : AbstractDependencyContainer, IDisposable
    {
        public DependencyContainer(IDependencyContext parent) : base(parent)
        {
            DependencyDescriptorProvider = new DefaultDependencyDescriptorProvider();
        }

        protected override IDependencyDescriptorProvider DependencyDescriptorProvider { get; }
    }
}
#endif