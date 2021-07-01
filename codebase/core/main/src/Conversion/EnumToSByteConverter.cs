using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="sbyte"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToSByteConverter<T> : AbstractTwoWayConverter<T, sbyte> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToSByteConverter{T}"/> instance.
        /// </summary>
        public static readonly EnumToSByteConverter<T> Instance = new EnumToSByteConverter<T>();

        /// <inheritdoc />
        protected override sbyte DoConvert(T source) => (sbyte) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(sbyte source) => (T) (object) source;
    }
}