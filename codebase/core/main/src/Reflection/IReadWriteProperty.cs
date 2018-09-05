#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected read-write property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    public interface IReadWriteProperty : IProperty, IReadWriteMember { }
}
#endif