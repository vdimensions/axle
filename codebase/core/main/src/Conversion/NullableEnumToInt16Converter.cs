using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="short"/> representation.
    /// </summary>
    /// <see cref="EnumToInt16Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt16Converter<T> : DelegatingTwoWayConverter<T?, short?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt16Converter{T}"/> class.
        /// </summary>
        public NullableEnumToInt16Converter() 
            : base(new NullableToStructTwoWayConverter<T, short>(new EnumToInt16Converter<T>())) { }
    }
}