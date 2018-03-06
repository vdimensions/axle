using System.Collections.Generic;


namespace Axle.DependencyInjection.Sdk
{
    public interface IFactoryDescriptor
    {
        IList<IFactoryArgumentDescriptor> Arguments { get; }
    }
}