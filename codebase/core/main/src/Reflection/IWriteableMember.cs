namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a type member that allows updating its value via a <see cref="ISetAccessor">set accessor</see>. 
    /// A writable member usually represents a field or property.
    /// </summary>
    /// <seealso cref="IField"/>
    /// <seealso cref="IProperty"/>
    /// <seealso cref="ISetAccessor"/>
    public interface IWriteableMember : IMember, IAccessible
    {
        /// <summary>
        /// The accessor used to update the member's value. 
        /// </summary>
        ISetAccessor SetAccessor { get; }
    }
}