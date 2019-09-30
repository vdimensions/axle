#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.IO;
#if NETFRAMEWORK
using System.Runtime.Remoting.Messaging;
#endif
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;


namespace Axle.IO.Serialization
{
    /// <summary>
    /// An <see cref="ISerializer"/> implementation that uses the <see cref="SoapFormatter">binary formatter</see>
    /// that is built-in within the .NET framework.
    /// </summary>
    /// <seealso cref="SoapFormatter"/>
    [Serializable]
    public sealed class SoapSerializer : ISerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Axle.IO.Serialization.SoapSerializer"/> class.
        /// </summary>
        public SoapSerializer()
        {
            Formatter = new SoapFormatter();
        }

        /// <inheritdoc />
        public object Deserialize(Stream stream, Type objectType) => Formatter.Deserialize(stream);
        #if NETFRAMEWORK
        public object Deserialize(Stream stream, HeaderHandler handler) => Formatter.Deserialize(stream, handler);
        #endif

        /// <inheritdoc />
        public void Serialize(object obj, Stream stream) => Formatter.Serialize(stream, obj);
        #if NETFRAMEWORK
        public void Serialize(object obj, Stream stream, Header[] headers) => Formatter.Serialize(stream, obj, headers);
        #endif

        /// <summary>
        /// Gets a reference to the <see cref="SoapFormatter"/> instance that is used to serialize 
        /// and deserialize objects.
        /// </summary>
        public SoapFormatter Formatter { get; }
    }
}
#endif