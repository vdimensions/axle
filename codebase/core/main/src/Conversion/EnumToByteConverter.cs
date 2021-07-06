using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="byte"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToByteConverter<T> : AbstractTwoWayConverter<T, byte> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToByteConverter{T}"/> instance.
        /// </summary>
        public static readonly EnumToByteConverter<T> Instance = new EnumToByteConverter<T>();

        /// <inheritdoc />
        protected override byte DoConvert(T source) => (byte) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(byte source) => (T) (object) source;
    }
}