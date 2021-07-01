using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="ulong"/> representation.
    /// </summary>
    /// <see cref="EnumToUInt64Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToUInt64Converter<T> : DelegatingTwoWayConverter<T?, ulong?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToUInt64Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToUInt64Converter<T> Instance = new NullableEnumToUInt64Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt64Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToUInt64Converter(EnumToUInt64Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, ulong>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToUInt64Converter{T}"/> class.
        /// </summary>
        public NullableEnumToUInt64Converter() : this(EnumToUInt64Converter<T>.Instance) { }
    }
}