using System.Collections.Generic;


namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public interface IFactoryDescriptor
    {
        IList<IFactoryArgumentDescriptor> Arguments { get; }
    }
}