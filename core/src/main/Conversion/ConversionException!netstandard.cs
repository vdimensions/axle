using System;
using System.Runtime.Serialization;


namespace Axle.Conversion
{
    [Serializable]
    partial class ConversionException
    {
        protected ConversionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}