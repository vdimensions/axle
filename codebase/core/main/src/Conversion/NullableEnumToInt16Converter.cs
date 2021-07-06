using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="short"/> representation.
    /// </summary>
    /// <see cref="EnumToInt16Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt16Converter<T> : DelegatingTwoWayConverter<T?, short?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToInt16Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToInt16Converter<T> Instance = new NullableEnumToInt16Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt16Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToInt16Converter(EnumToInt16Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, short>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt16Converter{T}"/> class.
        /// </summary>
        public NullableEnumToInt16Converter() : this(EnumToInt16Converter<T>.Instance) { }
    }
}