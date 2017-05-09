using System;
using System.Runtime.Serialization;


namespace Axle.Conversion.Parsing
{
    [Serializable]
    partial class ParseException
    {
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}