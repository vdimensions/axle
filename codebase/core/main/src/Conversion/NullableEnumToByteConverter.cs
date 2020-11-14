using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="byte"/> representation.
    /// </summary>
    /// <see cref="EnumToByteConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToByteConverter<T> : IConverter<T?, byte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private IConverter<T?, byte?> _converter = new NullableToStructConverter<T, byte>(new EnumToByteConverter<T>());

        /// <inheritdoc />
        public byte? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out byte? target) => _converter.TryConvert(source, out target);
    }
}