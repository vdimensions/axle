#if NETSTANDARD2_0_OR_NEWER || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Axle.IO.Serialization
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> interface using
    /// <see cref="DataContractSerializer">data contract</see> serialization as the underlying implementation.
    /// </summary>
    /// <seealso cref="DataContractSerializer"/>
    /// <seealso cref="ISerializer"/>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlContractSerializer : ISerializer
    {
        /// <inheritdoc />
        public object Deserialize(Stream stream, Type objectType)
        {
            var ser = new DataContractSerializer(objectType);
            using (var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
            {
                var result = ser.ReadObject(reader, true);
                reader.Close();
                return result;
            }
        }

        /// <inheritdoc />
        public void Serialize(object obj, Stream stream)
        {
            var ser = new DataContractSerializer(obj.GetType());
            ser.WriteObject(stream, obj);
        }
    }
}
#endif