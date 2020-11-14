using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="int"/> representation.
    /// </summary>
    /// <see cref="EnumToInt32Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt32Converter<T> : IConverter<T?, int?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        private IConverter<T?, int?> _converter = new NullableToStructConverter<T, int>(new EnumToInt32Converter<T>());

        /// <inheritdoc />
        public int? Convert(T? source) => _converter.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(T? source, out int? target) => _converter.TryConvert(source, out target);
    }
}