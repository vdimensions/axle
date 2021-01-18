using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="long"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class EnumToInt64Converter<T> : AbstractTwoWayConverter<T, long> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <inheritdoc />
        protected override long DoConvert(T source) => (long) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(long source) => (T) (object) source;
    }
}