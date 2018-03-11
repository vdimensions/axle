using System.Collections.Generic;

using Axle.Reflection;


namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public class FactoryDescriptor : IFactoryDescriptor
    {
        public FactoryDescriptor(IInvokable factory, IList<IFactoryArgumentDescriptor> arguments)
        {
            Factory = factory;
            Arguments = arguments;
        }

        public IInvokable Factory { get; }
        public IList<IFactoryArgumentDescriptor> Arguments { get; }
    }
}