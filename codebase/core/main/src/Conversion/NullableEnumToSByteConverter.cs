using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="sbyte"/> representation.
    /// </summary>
    /// <see cref="EnumToSByteConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToSByteConverter<T> : DelegatingTwoWayConverter<T?, sbyte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToSByteConverter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToSByteConverter<T> Instance = new NullableEnumToSByteConverter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToSByteConverter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToSByteConverter(EnumToSByteConverter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, sbyte>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToSByteConverter{T}"/> class.
        /// </summary>
        public NullableEnumToSByteConverter() : this(EnumToSByteConverter<T>.Instance) { }
    }
}