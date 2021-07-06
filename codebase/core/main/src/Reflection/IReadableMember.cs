namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected type member that allows reading its value via a 
    /// <see cref="IGetAccessor">get accessor</see>.
    /// A readable member usually represents a field or property.
    /// </summary>
    /// <seealso cref="IGetAccessor"/>
    /// <seealso cref="IAccessible"/>
    /// <seealso cref="IReadWriteMember"/>
    /// <seealso cref="IWriteableMember"/>
    /// <seealso cref="IField"/>
    /// <seealso cref="IProperty"/>
    /// <seealso cref="IReadOnlyProperty"/>
    /// <seealso cref="IReadWriteProperty"/>
    public interface IReadableMember : IMember, IAccessible
    {
        /// <summary>
        /// The accessor used to read the member's value.
        /// </summary>
        IGetAccessor GetAccessor { get; }
    }
}