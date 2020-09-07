#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Xml;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    public interface ISupportsDeserialize
    {
        void Deserialize(XmlReader xmlReader, bool serializeCollectionKey);
    }
}
#endif