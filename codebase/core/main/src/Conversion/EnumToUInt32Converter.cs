using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="uint"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToUInt32Converter<T> : AbstractTwoWayConverter<T, uint> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToUInt32Converter{T}"/> instance.
        /// </summary>
        public static readonly EnumToUInt32Converter<T> Instance = new EnumToUInt32Converter<T>();
        
        /// <inheritdoc />
        protected override uint DoConvert(T source) => (uint) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(uint source) => (T) (object) source;
    }
}