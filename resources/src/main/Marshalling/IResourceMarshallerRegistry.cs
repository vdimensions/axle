using System.Collections.Generic;


namespace Axle.Resources.Marshalling
{
    public interface IResourceMarshallerRegistry : IEnumerable<IResourceMarshaller>
    {
        IResourceMarshallerRegistry Register(IResourceMarshaller marshaller);
    }
}