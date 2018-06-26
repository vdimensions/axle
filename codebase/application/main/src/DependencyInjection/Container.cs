#if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
using System;

using Axle.Application.DependencyInjection.Sdk;


namespace Axle.Application.DependencyInjection
{
    public class Container : AbstractContainer, IDisposable
    {
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
        }

        public void Dispose() => Dispose(true);
        void IDisposable.Dispose() => Dispose(true);

        public Container(IContainer parent) : base(parent)
        {
            DependencyDescriptorProvider = new DefaultDependencyDescriptorProvider();
        }

        protected override IDependencyDescriptorProvider DependencyDescriptorProvider { get; }
    }
}
#endif