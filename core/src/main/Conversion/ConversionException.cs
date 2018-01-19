using System;


namespace Axle.Conversion
{
    public partial class ConversionException : Exception
    {
        public ConversionException() {}
        public ConversionException(string message) : base(message) {}
        public ConversionException(string message, Exception inner) : base(message, inner) {}

        public ConversionException(Type sourceType, Type destinationType) : this(
            string.Format("Cannot convert an instance of {0} to {1}.", sourceType.FullName, destinationType.FullName),
            null) {}
        public ConversionException(Type sourceType, Type destinationType, Exception inner) : this(
            string.Format("Cannot convert an instance of {0} to {1}.", sourceType.FullName, destinationType.FullName), 
            inner) {}
    }
}