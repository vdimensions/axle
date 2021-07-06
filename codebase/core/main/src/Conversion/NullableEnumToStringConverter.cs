using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="string"/> representation.
    /// </summary>
    /// <see cref="EnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableEnumToStringConverter<T> : DelegatingConverter<T?, string> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="NullableEnumToStringConverter{T}"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly NullableEnumToStringConverter<T> Instance = new NullableEnumToStringConverter<T>();
        
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToStringConverter{T}"/> class
        /// using the provided <paramref name="converter"/>.
        /// </summary>
        public NullableEnumToStringConverter(EnumToStringConverter<T> converter) 
            : base(new NullableToClassConverter<T, string>(converter)) { }
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToStringConverter{T}"/> class.
        /// </summary>
        public NullableEnumToStringConverter() : this(EnumToStringConverter<T>.Instance) { }
    }
}