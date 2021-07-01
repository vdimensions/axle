using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ushort"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt16Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt16Converter<T> : DelegatingTwoWayConverter<T?, ushort?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToUInt16Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToUInt16Converter<T> Instance = new NullableEnumToUInt16Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt16Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToUInt16Converter(EnumToUInt16Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, ushort>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt16Converter{T}"/> class.
        /// </summary>
        public NullableEnumToUInt16Converter() : this(EnumToUInt16Converter<T>.Instance) { }
    }
}