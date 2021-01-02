#if NETSTANDARD2_0_OR_NEWER || NET40_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Axle.IO.Serialization
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> interface using
    /// <see cref="DataContractJsonSerializer">data contract</see> serialization as the underlying implementation.
    /// </summary>
    /// <seealso cref="DataContractSerializer"/>
    /// <seealso cref="ISerializer"/>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class JsonContractSerializer : ISerializer
    {
        /// <inheritdoc />
        public object Deserialize(Stream stream, Type objectType)
        {
            var ser = new DataContractJsonSerializer(objectType);
            return ser.ReadObject(stream);
        }

        /// <inheritdoc />
        public void Serialize(object obj, Stream stream)
        {
            var ser = new DataContractJsonSerializer(obj.GetType());
            ser.WriteObject(stream, obj);
        }
    }
}
#endif