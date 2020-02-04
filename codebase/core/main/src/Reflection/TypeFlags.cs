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
    public enum TypeFlags : int
    {
        /// <summary>
        /// The type flags cannot be determined.
        /// </summary>
        Unknown             =       0,
        /// <summary>
        /// Marks disposable types; i.e. types implementing the <see cref="IDisposable"/> interface.
        /// </summary>
        Disposable          =       1,
        /// <summary>
        /// Represents value types. Those include the pre-defined in the .NET framework primitive types, such as <see cref="int"/>,
        /// as well as any user type defined as a <c>struct</c>. Value types cannot be represented by the 
        /// <c><see langword="null"/></c> value (with the exception of nullable value types).
        /// <seealso cref="System.ValueType"/>
        /// <seealso cref="NullableValueType"/>
        /// </summary>
        ValueType           = 1 <<  1,  // 2
        /// <summary>
        /// Represents reference types. Reference types are stored in the heap and accessed via a reference (pointer). A reference
        /// type's pointer may have the <c><see langword="null"/></c> value.
        /// </summary>
        ReferenceType       = 1 <<  2,  // 4
        /// <summary>
        /// Represents generic types.
        /// </summary>
        /// <seealso cref="GenericDefinition"/>
        Generic             = 1 <<  3,  // 8
        /// <summary>
        /// Represents enumerable types.
        /// </summary>
        Enumerable          = 1 <<  4,  // 16
        /// <summary>
        /// Represents array types.
        /// </summary>
        /// <seealso cref="GenericDefinition"/>
        Array               = 1 <<  5 | ReferenceType | Enumerable, // 52
        /// <summary>
        /// Represents enumeration types.
        /// </summary>
        /// <seealso cref="ValueTuple"/>
        Enum                = 1 <<  6 | ValueType, // 66
        /// <summary>
        /// Represents nullable value types. Nullable value types are generic type wrapper with
        /// the type <see cref="Nullable{}"/> as its <see cref="GenericDefinition">generic type definition</see>
        /// and a regular <see cref="ValueType"/> as the generic parameter. While semantically this is no different
        /// than other value types, the compiler permits assignment of a <c><see langword="null"/></c> value to instances
        /// of such nullable types.
        /// </summary>
        /// <seealso cref="ValueType"/>
        NullableValueType   = 1 <<  7 | ValueType | Generic, // 138
        /// <summary>
        /// Represents abstract reference types.
        /// </summary>
        Abstract            = 1 <<  8 | ReferenceType, // 260
        /// <summary>
        /// Represents non-inheritable reference types.
        /// </summary>
        /// <seealso cref="ReferenceType"/>
        /// <seealso cref="Interface"/>
        /// <seealso cref="Abstract"/>
        Sealed              = 1 <<  9 | ReferenceType, // 516
        /// <summary>
        /// Represents <see langword="static"/> types
        /// </summary>
        Static              = Abstract | Sealed, // 776
        /// <summary>
        /// Represents interface types.
        /// </summary>
        /// <seealso cref="ReferenceType"/>
        /// <seealso cref="Abstract"/>
        Interface           = 1 << 10 | Abstract, // 1284
        /// <summary>
        /// Represents delegate types.
        /// </summary>
        Delegate            = 1 << 11 | ReferenceType, // 2052
        /// <summary>
        /// Represents an <seealso cref="Attribute"/> type.
        /// </summary>
        Attribute           = 1 << 12 | ReferenceType, // 5000
        /// <summary>
        /// Represents a generic type definition. This is a non-instantiateable representation of a generic type with
        /// placeholder types in place of the type parameters.
        /// </summary>
        /// <seealso cref="Generic"/>
        GenericDefinition   = 1 << 13 | Generic, // 8200
        /// <summary>
        /// Represents a generic type parameter.
        /// </summary>
        /// <seealso cref="Generic"/>
        GenericParameter    = 1 << 14, // 16384
        /// <summary>
        /// Represents a nested type.
        /// </summary>
        Nested              = 1 << 15  // 32768
    }
}
