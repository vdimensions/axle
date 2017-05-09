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
        /// Gets the target object being represented by the current <see cref="IReference{T}"/> instance.
        /// </summary>
        new T Value { get; }
    }
}