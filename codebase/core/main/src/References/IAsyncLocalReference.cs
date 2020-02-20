#if (NETSTANDARD1_4_OR_NEWER || NET46_OR_NEWER) && !NET45
namespace Axle.References
{
    /// <summary>
    /// An interface representing a reference to ambient data, that is local to a given asynchronous control flow, such
    /// as asynchronous method. 
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <seealso cref="System.Threading.ThreadLocal{T}"/>
    public interface IAsyncLocalReference<T> : IReference<T>
    {
        /// <summary>
        /// Gets or sets the value of this instance on the current thread.
        /// </summary>
        new T Value { get; set; }
    }
}
#endif