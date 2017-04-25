using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration representing the possible access modifiers supported in the .NET framework.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
    //[Maturity(CodeMaturity.Stable)]
    public enum AccessModifier
    {
        /// <summary>
        /// An access modifier for publicly accessible members (<c>public</c>).
        /// </summary>
        Public,
        /// <summary>
        /// An access modifier for reflected members with <c>private</c> access.
        /// </summary>
        Private,
        /// <summary>
        /// An access modifier for reflected members with <c>protected</c> access.
        /// </summary>
        Protected,
        /// <summary>
        /// An access modifier for reflected members with <c>internal</c> access.
        /// </summary>
        Internal,
        /// <summary>
        /// An access modifier for reflected members with <c>protected internal</c> access.
        /// </summary>
        ProtectedInternal
    }
}