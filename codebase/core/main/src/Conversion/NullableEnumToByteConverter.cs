using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="byte"/> representation.
    /// </summary>
    /// <see cref="EnumToByteConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToByteConverter<T> : DelegatingTwoWayConverter<T?, byte?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToByteConverter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToByteConverter<T> Instance = new NullableEnumToByteConverter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToByteConverter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToByteConverter(EnumToByteConverter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, byte>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToByteConverter{T}"/> class.
        /// </summary>
        public NullableEnumToByteConverter() : this(EnumToByteConverter<T>.Instance) { }
    }
}