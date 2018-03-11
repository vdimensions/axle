using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a reflected member; that is, a type member obtained via reflection.
    /// </summary>
    /// <seealso cref="MemberInfo"/>
    public interface IReflected
    {
        /// <summary>
        /// A reference to the underlying reflected member.
        /// </summary>
        MemberInfo ReflectedMember { get; }
    }

    /// <summary>
    /// A generic interface representing a reflected member; that is, a type member obtained via reflection.
    /// </summary>
    /// <typeparam name="T">
    /// The actual type of the reflected member.
    /// </typeparam>
    /// <seealso cref="IReflected"/>
    public interface IReflected<T> : IReflected where T: MemberInfo
    {
        /// <summary>
        /// A reference to the underlying reflected member.
        /// </summary>
        new T ReflectedMember { get; }
    }
}