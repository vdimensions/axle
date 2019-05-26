namespace Axle.Reflection
{
    /// <summary>
    /// An enumeration representing the various accessor types that are supported by the .NET framework
    /// and visible trough reflection.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public enum AccessorType : byte
    {
        /// <summary>
        /// Represents the <see langword="get" /> accessor, typically associated with property getters.
        /// </summary>
        Get,

        /// <summary>
        /// Represents the <see langword="set" /> accessor, typically associated with property setters.
        /// </summary>
        Set, 

        /// <summary>
        /// Represents the <see langword="add" /> accessor, typically associated with events and delegates.
        /// </summary>
        Add,

        /// <summary>
        /// Represents the <see langword="remove" /> accessor, typically associated with events and delegates.
        /// </summary>
        Remove
    }
}