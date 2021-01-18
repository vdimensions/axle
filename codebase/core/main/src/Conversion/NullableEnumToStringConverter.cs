using System;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values of a nullable enumeration type <typeparam name="T"/>
    /// and their <see cref="string"/> representation.
    /// </summary>
    /// <see cref="EnumToStringConverter{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableEnumToStringConverter<T> : DelegatingConverter<T?, string> 
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
        where T: struct, IComparable, IConvertible, IFormattable
        #else
        where T: struct, IComparable, IFormattable
        #endif
    {
        /// <summary>
        /// Initialized a new instance of the <see cref="NullableEnumToStringConverter{T}"/> class.
        /// </summary>
        public NullableEnumToStringConverter() 
            : base(new NullableToClassConverter<T, string>(new EnumToStringConverter<T>())) { }
    }
}