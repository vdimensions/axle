namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration representing the possible access modifiers supported in the .NET framework.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [System.Serializable]
    #endif
    public enum AccessModifier : byte
    {
        /// <summary>
        /// An access modifier for reflected members with <see langword="public"/> access.
        /// </summary>
        Public,

        /// <summary>
        /// An access modifier for reflected members with <see langword="private"/> access.
        /// </summary>
        Private,

        /// <summary>
        /// An access modifier for reflected members with <see langword="protected"/> access.
        /// </summary>
        Protected,

        /// <summary>
        /// An access modifier for reflected members with <see langword="internal"/> access.
        /// </summary>
        Internal,

        /// <summary>
        /// An access modifier for reflected members with <see langword="protected internal"/> access.
        /// </summary>
        ProtectedInternal
    }
}