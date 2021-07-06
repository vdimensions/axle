#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Xml;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    internal interface ISupportsDeserializeInternal
    {
        void Deserialize(XmlReader reader, bool serializeCollectionKey);
    }
}
#endif