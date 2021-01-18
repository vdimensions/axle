using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="byte"/> representation.
    /// </summary>
    /// <see cref="EnumToByteConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableEnumToByteConverter<T> : DelegatingTwoWayConverter<T?, byte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToByteConverter{T}"/> class.
        /// </summary>
        public NullableEnumToByteConverter() 
            : base(new NullableToStructTwoWayConverter<T, byte>(new EnumToByteConverter<T>())) { }
    }
}