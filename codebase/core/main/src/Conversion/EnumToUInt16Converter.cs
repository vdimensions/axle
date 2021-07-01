using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="ushort"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToUInt16Converter<T> : AbstractTwoWayConverter<T, ushort> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToUInt16Converter{T}"/> instance.
        /// </summary>
        public static readonly EnumToUInt16Converter<T> Instance = new EnumToUInt16Converter<T>();
        
        /// <inheritdoc />
        protected override ushort DoConvert(T source) => (ushort) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(ushort source) => (T) (object) source;
    }
}