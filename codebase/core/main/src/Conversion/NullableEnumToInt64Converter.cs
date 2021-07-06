using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="long"/> representation.
    /// </summary>
    /// <see cref="EnumToInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt64Converter<T> : DelegatingTwoWayConverter<T?, long?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToInt64Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToInt64Converter<T> Instance = new NullableEnumToInt64Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt64Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToInt64Converter(EnumToInt64Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, long>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt64Converter{T}"/> class.
        /// </summary>
        public NullableEnumToInt64Converter() : this(EnumToInt64Converter<T>.Instance) { }
    }
}