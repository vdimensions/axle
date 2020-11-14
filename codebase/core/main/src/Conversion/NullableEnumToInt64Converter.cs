using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="long"/> representation.
    /// </summary>
    /// <see cref="EnumToInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt64Converter<T> : ITwoWayConverter<T?, long?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private ITwoWayConverter<T?, long?> _converter = new NullableToStructTwoWayConverter<T, long>(new EnumToInt64Converter<T>());

        /// <inheritdoc />
        public long? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out long? target) => _converter.TryConvert(source, out target);

        /// <inheritdoc />
        public T? ConvertBack(long? obj) => _converter.ConvertBack(obj);

        /// <inheritdoc />
        public bool TryConvertBack(long? obj, out T? result) => _converter.TryConvertBack(obj, out result);

        /// <inheritdoc />
        public ITwoWayConverter<long?, T?> Invert() => _converter.Invert();
    }
}