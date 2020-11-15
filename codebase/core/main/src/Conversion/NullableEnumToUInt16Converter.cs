using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ushort"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt16Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt16Converter<T> : DelegatingTwoWayConverter<T?, ushort?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        public NullableEnumToUInt16Converter() 
            : base(new NullableToStructTwoWayConverter<T, ushort>(new EnumToUInt16Converter<T>())) { }
    }
}