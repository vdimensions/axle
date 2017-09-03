using System.Collections.Generic;

using Axle.Reflection;


namespace Axle.DependencyInjection.Sdk
{
    public class FactoryDescriptor
    {
        public FactoryDescriptor(IInvokable factory, IList<FactoryArgumentDescriptor> arguments)
        {
            Factory = factory;
            Arguments = arguments;
        }

        public IInvokable Factory { get; }
        public IList<FactoryArgumentDescriptor> Arguments { get; }
    }
}