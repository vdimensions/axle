using System.Configuration;
using System.Xml;


namespace Axle.Configuration.Sdk
{
    internal delegate void DeserializeMethod<T>(T element, XmlReader reader, bool serializeCollectionKey) where T: ConfigurationElement;
}