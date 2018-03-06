using System.Xml;


namespace Axle.Configuration.Sdk
{
    public interface ISupportsDeserialize
    {
        void Deserialize(XmlReader xmlReader, bool serializeCollectionKey);
    }
}