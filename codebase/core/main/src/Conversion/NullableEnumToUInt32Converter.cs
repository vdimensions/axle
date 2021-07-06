using System;
using System.Diagnostics.CodeAnalysis;

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
    public sealed class NullableEnumToUInt32Converter<T> : DelegatingTwoWayConverter<T?, uint?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToUInt32Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToUInt32Converter<T> Instance = new NullableEnumToUInt32Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt32Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToUInt32Converter(EnumToUInt32Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, uint>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt32Converter{T}"/> class.
        /// </summary>
        public NullableEnumToUInt32Converter() : this(EnumToUInt32Converter<T>.Instance) { }
    }
}