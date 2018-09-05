#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a setter method (or setter) for a class member (a field or property).
    /// </summary>
    /// <seealso cref="AccessorType.Set">AccessorType.Set</seealso>
    /// <seealso cref="IGetAccessor" />
    /// <seealso cref="IAccessor" />
    /// <seealso cref="IWriteableMember" />
    //[Maturity(CodeMaturity.Stable)]
    public interface ISetAccessor : IAccessor
    {
        /// <summary>
        /// Sets the value of the represented <see cref="IField">field</see> or <see cref="IProperty">property</see> 
        /// to the object specified by the <paramref name="value"/> object.
        /// </summary>
        /// <param name="target">
        /// The target object that owns the member whose value is being set. 
        /// </param>
        /// <param name="value">
        /// The value to be set. 
        /// </param>
        void SetValue(object target, object value);
    }
}
#endif