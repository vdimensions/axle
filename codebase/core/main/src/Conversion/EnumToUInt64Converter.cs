using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="ulong"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToUInt64Converter<T> : AbstractTwoWayConverter<T, ulong> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToUInt64Converter{T}"/> instance.
        /// </summary>
        public static readonly EnumToUInt64Converter<T> Instance = new EnumToUInt64Converter<T>();
        
        /// <inheritdoc />
        protected override ulong DoConvert(T source) => (ulong) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(ulong source) => (T) (object) source;
    }
}