#if NETSTANDARD || NET20_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
namespace Axle.References
{
    /// <summary>
    /// An interface representing a singleton reference object.
    /// According to the singleton design pattern, a singleton object has only one instance throughout the execution of
    /// the application.
    /// </summary>
    /// <seealso cref="ISingletonReference{T}"/>
    /// <seealso cref="Singleton{T}"/>
    /// <seealso cref="Singleton"/>
    public interface ISingletonReference : IReference { }

    /// <summary>
    /// A generic variant of the <see cref="ISingletonReference"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the singleton object represented by the current <see cref="ISingletonReference{T}"/> implementation.
    /// </typeparam>
    /// <seealso cref="Singleton{T}"/>
    /// <seealso cref="Singleton"/>
    /// <seealso cref="ISingletonReference"/>
    public interface ISingletonReference<T> : IReference<T>, ISingletonReference { }
}
#endif