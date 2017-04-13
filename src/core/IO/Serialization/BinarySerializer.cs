using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Axle.IO.Serialization
{
    /// <summary>
    /// An <see cref="ISerializer"/> implementation that uses the <see cref="BinaryFormatter">binary formatter</see>
    /// that is built-in within the .NET framework to serialize objects.
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
        /// Initializes a new instance of the <see cref="Axle.IO.Serialization.BinarySerializer"/> class with a given selector and streaming context.
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

        public object Deserialize(Stream stream, Type objectType) { return Formatter.Deserialize(stream); }
        public object Deserialize(Stream stream, HeaderHandler handler) { return Formatter.Deserialize(stream, handler); }

        public void Serialize(object obj, Stream stream) { Formatter.Serialize(stream, obj); }
        public void Serialize(object obj, Stream stream, Header[] headers) { Formatter.Serialize(stream, obj, headers); }

        /// <summary>
        /// Gets a reference to the <see cref="BinaryFormatter"/> instance that is used to serialize 
        /// and deserialize objects.
        /// </summary>
        public BinaryFormatter Formatter { get; private set; }
    }
}