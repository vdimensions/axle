using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="int"/> representation.
    /// </summary>
    /// <see cref="EnumToInt32Converter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToInt32Converter<T> : DelegatingTwoWayConverter<T?, int?> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToInt32Converter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToInt32Converter<T> Instance = new NullableEnumToInt32Converter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt32Converter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToInt32Converter(EnumToInt32Converter<T> converter) 
            : base(new NullableToStructTwoWayConverter<T, int>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToInt32Converter{T}"/> class.
        /// </summary>
        public NullableEnumToInt32Converter() : this(EnumToInt32Converter<T>.Instance) { }
    }
}