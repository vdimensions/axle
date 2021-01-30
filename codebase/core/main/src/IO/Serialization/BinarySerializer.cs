#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.IO;
#if NETFRAMEWORK
using System.Runtime.Remoting.Messaging;
#endif
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Axle.IO.Serialization
{
    /// <summary>
    /// An <see cref="ISerializer"/> implementation that uses the <see cref="BinaryFormatter">binary formatter</see>
    /// that is built-in within the .NET framework.
    /// </summary>
    /// <seealso cref="BinaryFormatter"/>
    [Serializable]
    public sealed class BinarySerializer : ISerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Axle.IO.Serialization.BinarySerializer"/> class.
        /// </summary>
        public BinarySerializer()
        {
            Formatter = new BinaryFormatter();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Axle.IO.Serialization.BinarySerializer"/> class with a given 
        /// selector and streaming context.
        /// </summary>
        /// <param name="selector">
        /// The <see cref="ISurrogateSelector"/> to use. Can be <c>null</c>. 
        /// </param>
        /// <param name="context">
        /// The source and destination for the serialied data. 
        /// </param>
        public BinarySerializer(ISurrogateSelector selector, StreamingContext context)
        {
            Formatter = new BinaryFormatter(selector, context);
        }

        /// <inheritdoc />
        public object Deserialize(Stream stream, Type objectType) => Formatter.Deserialize(stream);
        #if NETFRAMEWORK
        /// <summary>
        /// Deserializes the specified <paramref name="stream"/> into an object graph. The provided header 
        /// <paramref name="handler"/> handles any headers present in the stream.
        /// </summary>
        /// <param name="stream">
        /// The stream providing the data to deserialize.
        /// </param>
        /// <param name="handler">
        /// A <see cref="HeaderHandler"/> instance.
        /// </param>
        /// <returns>
        /// An object instance representing the deserialized result.
        /// </returns>
        public object Deserialize(Stream stream, HeaderHandler handler) => Formatter.Deserialize(stream, handler);
        #endif

        /// <inheritdoc />
        public void Serialize(object obj, Stream stream) => Formatter.Serialize(stream, obj);
        #if NETFRAMEWORK
        /// <summary>
        /// Serializes an object <paramref name="graph"/> into the specified <paramref name="stream"/>, 
        /// attaching the provided <paramref name="headers"/>.
        /// </summary>
        /// <param name="graph">
        /// The object to be serialized.
        /// </param>
        /// <param name="stream">
        /// The target <paramref name="stream"/> to serialize the object to.
        /// </param>
        /// <param name="headers">
        /// Remoting headers to include in the serialization. Can be <c><see langword="null"/></c>.
        /// </param>
        public void Serialize(object graph, Stream stream, Header[] headers) => 
            Formatter.Serialize(stream, graph, headers);
        #endif

        /// <summary>
        /// Gets a reference to the <see cref="BinaryFormatter"/> instance that is used to serialize 
        /// and deserialize objects.
        /// </summary>
        public BinaryFormatter Formatter { get; }
    }
}
#endif