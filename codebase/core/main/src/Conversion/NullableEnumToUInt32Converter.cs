using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="uint"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt32Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt32Converter<T> : IConverter<T?, uint?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private IConverter<T?, uint?> _converter = new NullableToStructConverter<T, uint>(new EnumToUInt32Converter<T>());

        /// <inheritdoc />
        public uint? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out uint? target) => _converter.TryConvert(source, out target);
    }
}