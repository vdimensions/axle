using System;
using System.IO;
using System.Runtime.Serialization;


namespace Axle.IO.Serialization
{
    /// <summary>
    /// An interface representing a serializer; that is, an object that serializes and de-serializes objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserializes the contents of the given stream to an instance of the provided by the <paramref name="objectType"/> parameter type.
        /// </summary>
        /// <param name="stream">
        /// The stream containing the data to deserialize.
        /// </param>
        /// <param name="objectType">
        /// The type of the object to be deserialized.
        /// </param>
        /// <returns>
        /// An instance of the provided by the <paramref name="objectType"/> type as deserialized from the <paramref name="stream"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="stream"/> or <paramref name="objectType"/> is <c>null</c>.
        /// </exception>
        object Deserialize(Stream stream, Type objectType);

        /// <summary>
        /// Serializes the provided by the <see cref="obj"/> parameter object or graph of objects 
        /// to the target <paramref name="stream"/>. 
        /// </summary>
        /// <param name="obj">
        /// The object to be serialized.
        /// </param>
        /// <param name="stream">
        /// The <see cref="Stream"/> to serialize the object into.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="obj"/> or <paramref name="stream"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="SerializationException">
        /// An error has occurred durring the serialization process.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// The caller does not have the required permissions
        /// </exception>
        void Serialize(object obj, Stream stream);
    }
}