using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="sbyte"/> representation.
    /// </summary>
    /// <see cref="EnumToSByteConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToSByteConverter<T> : DelegatingTwoWayConverter<T?, sbyte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToSByteConverter{T}"/> class.
        /// </summary>
        public NullableEnumToSByteConverter() 
            : base(new NullableToStructTwoWayConverter<T, sbyte>(new EnumToSByteConverter<T>())) { }
    }
}