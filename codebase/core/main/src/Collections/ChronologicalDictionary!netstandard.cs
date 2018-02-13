using System;
using System.Runtime.Serialization;

// ReSharper disable UnusedTypeParameter
namespace Axle.Collections
{
    [Serializable]
    partial class ChronologicalDictionary<TKey, TValue>
    {
        [Serializable]
        partial class TimestampDictionary : ISerializable
        {
            internal TimestampDictionary(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
            {
                _collection = this;
            }

            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => GetObjectData(info, context);
        }
    }
}