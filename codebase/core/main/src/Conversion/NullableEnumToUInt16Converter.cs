using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ushort"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt16Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt16Converter<T> : ITwoWayConverter<T?, ushort?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private ITwoWayConverter<T?, ushort?> _converter = new NullableToStructTwoWayConverter<T, ushort>(new EnumToUInt16Converter<T>());

        /// <inheritdoc />
        public ushort? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out ushort? target) => _converter.TryConvert(source, out target);

        /// <inheritdoc />
        public T? ConvertBack(ushort? obj) => _converter.ConvertBack(obj);

        /// <inheritdoc />
        public bool TryConvertBack(ushort? obj, out T? result) => _converter.TryConvertBack(obj, out result);

        /// <inheritdoc />
        public ITwoWayConverter<ushort?, T?> Invert() => _converter.Invert();
    }
}