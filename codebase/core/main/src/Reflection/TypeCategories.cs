using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration describing the possible categories of a <see cref="Type"/>.
    /// </summary>
    [Flags]
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public enum TypeCategories : short
    {
        /// <summary>
        /// Default type category.
        /// </summary>
        Unknown             =       0,
        /// <summary>
        /// Represents value types
        /// </summary>
        ValueType           = 1 <<  1,  // 2
        /// <summary>
        /// Represents reference types
        /// </summary>
        ReferenceType       = 1 <<  2,  // 4
        /// <summary>
        /// Represents genric types
        /// </summary>
        Generic             = 1 <<  3,  // 8
        /// <summary>
        /// Represents array types
        /// </summary>
        /// <seealso cref="GenericDefinition"/>
        Array               = 1 <<  4 | ReferenceType, // 20
        /// <summary>
        /// Represents enumeration types
        /// </summary>
        /// <seealso cref="ValueTuple"/>
        Enum                = 1 <<  5 | ValueType, // 34
        /// <summary>
        /// Represens nullable value types types
        /// </summary>
        /// <seealso cref="ValueType"/>
        NullableValueType   = 1 <<  6 | ValueType | Generic, // 76
        /// <summary>
        /// Represents abstract reference types
        /// </summary>
        Abstract            = 1 <<  7 | ReferenceType, // 132
        /// <summary>
        /// Represents non-inheritable reference types
        /// </summary>
        /// <seealso cref="ReferenceType"/>
        /// <seealso cref="Interface"/>
        /// <seealso cref="Abstract"/>
        Sealed              = 1 <<  8 | ReferenceType, // 260
        /// <summary>
        /// Represents <see langword="static"/> types
        /// </summary>
        Static              = Abstract | Sealed, // 392
        /// <summary>
        /// Represents interface types
        /// </summary>
        /// <seealso cref="ReferenceType"/>
        /// <seealso cref="Abstract"/>
        Interface = 1 <<  9 | Abstract, // 644
        /// <summary>
        /// Represents delegate types
        /// </summary>
        Delegate            = 1 << 10 | ReferenceType, // 1028
        /// <summary>
        /// Represents an <seealso cref="Attribute"/> type
        /// </summary>
        Attribute = 1 << 11 | ReferenceType, // 2052
        /// <summary>
        /// Represents generic type definitions. This is a non-instantiateable raw type describing a generic type.
        /// </summary>
        /// <seealso cref="Generic"/>
        GenericDefinition   = 1 << 12 | Generic // 5004
    }
}
