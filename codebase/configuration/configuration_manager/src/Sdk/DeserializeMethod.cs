#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Xml;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    internal delegate void DeserializeMethod<T>(T element, XmlReader reader, bool serializeCollectionKey) 
        where T: ConfigurationElement;
}
#endif