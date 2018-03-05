#if NETSTANDARD1_6_OR_NEWER || !NETSTANDARD
using System;
using System.IO;


namespace Axle.IO.Serialization
{
    /// <summary>
    /// Serializes and deserializes objects into and from XML documents. 
    /// <para>
    /// This <see cref="ISerializer"/> implementation uses <see cref="System.Xml.Serialization.XmlSerializer" /> for the 
    /// actual serialization and deserialization
    /// </para>
    /// </summary>
    /// <seealso cref="System.Xml.Serialization.XmlSerializer" />
    /// <seealso cref="ISerializer" />
    //[Maturity(CodeMaturity.Frozen)]
    public sealed class XmlSerializer : ISerializer
    {
        /// <summary>
        /// Deserializes the XML document contained by the specified Stream. 
        /// </summary>
        /// <param name="stream">
        /// The <see cref="Stream"/> that contains the XML document to deserialize. 
        /// </param>
        /// <param name="objectType">
        /// The type of the object that the <paramref name="stream"/> will be deserialized to.
        /// </param>
        /// <returns>
        /// The <see cref="object">object</see> being deserialized. 
        /// </returns>
        public object Deserialize(Stream stream, Type objectType)
        {
            return new System.Xml.Serialization.XmlSerializer(objectType).Deserialize(stream);
        }

        /// <summary>
        /// Serializes the specified object and writes the XML document to the specified stream. 
        /// </summary>
        /// <param name="obj">
        /// The <see cref="object"/> to serialize. 
        /// </param>
        /// <param name="stream">
        /// The <see cref="Stream"/> used to write the XML document. 
        /// </param>
        public void Serialize(object obj, Stream stream)
        {
            var xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            xs.Serialize(stream, obj);
        }
    }
}
#endif