namespace Axle.Collections.ReadOnly
{
    /// <summary>
    /// Represents a strongly-typed read-only collection of values.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    #if NETSTANDARD
    public interface IReadOnlyCollection<out T> : System.Collections.Generic.IReadOnlyCollection<T>
    {
    }
    #else
    #if NET40_OR_NEWER
    public interface IReadOnlyCollection<out T> : System.Collections.Generic.IEnumerable<T>
    #else
    public interface IReadOnlyCollection<T> : System.Collections.Generic.IEnumerable<T>
    #endif
    {
        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        int Count { get; }
    }
    #endif
}