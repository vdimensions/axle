#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected event member.
    /// </summary>
    /// <seealso cref="System.Reflection.EventInfo"/>
    public interface IEvent : ICombineRemoveMember, IAttributeTarget { }
}
#endif