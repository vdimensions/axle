using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="long"/> representation.
    /// </summary>
    /// <see cref="EnumToInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableEnumToInt64Converter<T> : DelegatingTwoWayConverter<T?, long?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt64Converter{T}"/> class.
        /// </summary>
        public NullableEnumToInt64Converter() 
            : base(new NullableToStructTwoWayConverter<T, long>(new EnumToInt64Converter<T>())) { }
    }
}