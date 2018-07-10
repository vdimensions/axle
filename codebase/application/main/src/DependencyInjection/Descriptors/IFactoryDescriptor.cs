using System.Collections.Generic;


namespace Axle.DependencyInjection.Descriptors
{
    public interface IFactoryDescriptor
    {
        object CreateInstance(params object[] args);

        IList<IFactoryArgumentDescriptor> Arguments { get; }
    }
}