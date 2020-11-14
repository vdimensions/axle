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
    public sealed class NullableEnumToInt16Converter<T> : ITwoWayConverter<T?, short?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private ITwoWayConverter<T?, short?> _converter = new NullableToStructTwoWayConverter<T, short>(new EnumToInt16Converter<T>());

        /// <inheritdoc />
        public short? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out short? target) => _converter.TryConvert(source, out target);

        /// <inheritdoc />
        public T? ConvertBack(short? obj) => _converter.ConvertBack(obj);

        /// <inheritdoc />
        public bool TryConvertBack(short? obj, out T? result) => _converter.TryConvertBack(obj, out result);

        /// <inheritdoc />
        public ITwoWayConverter<short?, T?> Invert() => _converter.Invert();
    }
}