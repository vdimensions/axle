namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected type member that allows reading its value via a 
    /// <see cref="IGetAccessor">get accessor</see> and updating the value trough a <see cref="ISetAccessor"/>.
    /// A read-write member usually represents a non-readonly field or a property with both a getter and setter defined.
    /// </summary>
    /// <seealso cref="IField"/>
    /// <seealso cref="IGetAccessor"/>
    /// <seealso cref="ISetAccessor"/>
    /// <seealso cref="IAccessible"/>
    /// <seealso cref="IReadableMember"/>
    /// <seealso cref="IWriteableMember"/>
    /// <seealso cref="IProperty"/>
    /// <seealso cref="IReadWriteProperty"/>
    public interface IReadWriteMember : IReadableMember, IWriteableMember
    {
    }
}
