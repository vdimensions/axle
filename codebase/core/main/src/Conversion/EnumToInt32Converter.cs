using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="int"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToInt32Converter<T> : AbstractTwoWayConverter<T, int> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToInt32Converter{T}"/> instance.
        /// </summary>
        public static readonly EnumToInt32Converter<T> Instance = new EnumToInt32Converter<T>();

        /// <inheritdoc />
        protected override int DoConvert(T source) => (int) ((object) source);

        /// <inheritdoc />
        protected override T DoConvertBack(int source) => (T) (object) source;
    }
}