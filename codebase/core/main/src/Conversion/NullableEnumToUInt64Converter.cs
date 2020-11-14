using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ulong"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt64Converter<T> : ITwoWayConverter<T?, ulong?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private ITwoWayConverter<T?, ulong?> _converter = new NullableToStructTwoWayConverter<T, ulong>(new EnumToUInt64Converter<T>());

        /// <inheritdoc />
        public ulong? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out ulong? target) => _converter.TryConvert(source, out target);

        /// <inheritdoc />
        public T? ConvertBack(ulong? obj) => _converter.ConvertBack(obj);

        /// <inheritdoc />
        public bool TryConvertBack(ulong? obj, out T? result) => _converter.TryConvertBack(obj, out result);

        /// <inheritdoc />
        public ITwoWayConverter<ulong?, T?> Invert() => _converter.Invert();
    }
}