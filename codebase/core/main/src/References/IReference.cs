#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.References
{
    /// <summary>
    /// An interface representing a reference to an object.
    /// </summary>
    public interface IReference
    {
        /// <summary>
        /// Gets the target object being represented by the current <see cref="IReference"/> instance.
        /// </summary>
        object Value { get; }
    }

    /// <summary>
    /// A generic version of the <see cref="IReference" /> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the underlying object being referenced by the current <see cref="IReference{T}"/> instance.
    /// </typeparam>
    /// <seealso cref="IReference" />
    public interface IReference<T> : IReference
    {
        /// <summary>
        /// Tries to retrieve the value that is referenced by the current <see cref="IReference{T}"/> object.
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the reference value, if it is available. This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the target value was retrieved; <c>false</c> otherwise.
        /// </returns>
        bool TryGetValue(out T value);

        /// <summary>
        /// Gets the target object being represented by the current <see cref="IReference{T}"/> instance.
        /// </summary>
        new T Value { get; }
    }
}
#endif