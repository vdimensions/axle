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
    public sealed class NullableEnumToSByteConverter<T> : ITwoWayConverter<T?, sbyte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private ITwoWayConverter<T?, sbyte?> _converter = new NullableToStructTwoWayConverter<T, sbyte>(new EnumToSByteConverter<T>());

        /// <inheritdoc />
        public sbyte? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out sbyte? target) => _converter.TryConvert(source, out target);

        /// <inheritdoc />
        public T? ConvertBack(sbyte? obj) => _converter.ConvertBack(obj);

        /// <inheritdoc />
        public bool TryConvertBack(sbyte? obj, out T? result) => _converter.TryConvertBack(obj, out result);

        /// <inheritdoc />
        public ITwoWayConverter<sbyte?, T?> Invert() => _converter.Invert();
    }
}