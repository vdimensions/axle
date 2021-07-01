using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of an enumeration type <typeparam name="T"/>
    /// and their <see cref="string"/> representation.
    /// </summary>
    /// <see cref="NullableEnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class EnumToStringConverter<T> : AbstractConverter<T, string> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="EnumToStringConverter{T}"/> instance.
        /// </summary>
        public static readonly EnumToStringConverter<T> Instance = new EnumToStringConverter<T>();

        /// <inheritdoc />
        protected override string DoConvert(T source) => Enum.GetName(typeof(T), source);
    }
}