#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a type member that allows updating its value via a <see cref="ISetAccessor">set accessor</see>. 
    /// A writable member usually represents a field or property with defined setter.
    /// </summary>
    /// <seealso cref="IField"/>
    /// <seealso cref="ISetAccessor"/>
    /// <seealso cref="IReadableMember"/>
    /// <seealso cref="IReadWriteMember"/>
    /// <seealso cref="IProperty"/>
    /// <seealso cref="IReadWriteProperty"/>
    /// <seealso cref="IWriteOnlyProperty"/>
    public interface IWriteableMember : IMember, IAccessible
    {
        /// <summary>
        /// The accessor used to update the member's value. 
        /// </summary>
        ISetAccessor SetAccessor { get; }
    }
}
#endif