using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="long"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToInt64Converter<T> : AbstractTwoWayConverter<T, long> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToInt64Converter{T}"/> instance.
        /// </summary>
        public static readonly EnumToInt64Converter<T> Instance = new EnumToInt64Converter<T>();

        /// <inheritdoc />
        protected override long DoConvert(T source) => (long) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(long source) => (T) (object) source;
    }
}