using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ulong"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt64Converter<T> : DelegatingTwoWayConverter<T?, ulong?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt64Converter{T}"/> class.
        /// </summary>
        public NullableEnumToUInt64Converter() 
            : base(new NullableToStructTwoWayConverter<T, ulong>(new EnumToUInt64Converter<T>())) { }
    }
}