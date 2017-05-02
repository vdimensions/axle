using System;
using System.Runtime.Serialization;


namespace Axle.Conversion.Parsing
{
    [Serializable]
    partial class ParseException : FormatException
    {
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}