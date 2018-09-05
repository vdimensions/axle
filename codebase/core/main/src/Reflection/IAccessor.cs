#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing an accessor to a reflected member (e.g. getters, setters, event manipulators, etc.)
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// Gets the <see cref="DeclarationType">declaration type</see> of the current <see cref="IAccessor">accessor</see> instance.
        /// </summary>
        DeclarationType Declaration { get; }

        /// <summary>
        /// Gets the <see cref="Axle.Reflection.AccessModifier">access modifier</see> of the current <see cref="IAccessor">accessor</see> instance.
        /// </summary>
        AccessModifier AccessModifier { get; }

        /// <summary>
        /// Gets the <see cref="Axle.Reflection.AccessorType">accessor type</see> of the current <see cref="IAccessor">accessor</see> instance.
        /// </summary>
        AccessorType AccessorType { get; }

        /// <summary>
        /// Gets a reference to the reflected <see cref="IMember">member</see> the current <see cref="IAccessor">accessor</see> instance points to.
        /// </summary>
        IMember Member { get; }
    }
}
#endif