#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected property member.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo"/>
    public interface IProperty : IMember, IAttributeTarget { }
}
#endif