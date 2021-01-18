using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="uint"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt32Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt32Converter<T> : DelegatingTwoWayConverter<T?, uint?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt32Converter{T}"/> class.
        /// </summary>
        public NullableEnumToUInt32Converter() 
            : base(new NullableToStructTwoWayConverter<T, uint>(new EnumToUInt32Converter<T>())) { }
    }
}