using System.Xml;


namespace Axle.Configuration.Sdk
{
    internal interface ISupportsDeserializeInternal
    {
        void Deserialize(XmlReader reader, bool serializeCollectionKey);
    }
}