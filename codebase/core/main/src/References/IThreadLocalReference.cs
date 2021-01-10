#if NETSTANDARD || NET40_OR_NEWER || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
using System;

namespace Axle.References
{
    /// <summary>
    /// An interface representing a reference to a thread-local value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <seealso cref="System.Threading.ThreadLocal{T}"/>
    public interface IThreadLocalReference<T> : IReference<T>, IEquatable<IThreadLocalReference<T>>, IEquatable<T>
    {
        /// <summary>
        /// Gets or sets the value of this instance on the current thread.
        /// </summary>
        new T Value { get; set; }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="IThreadLocalReference{T}.Value"/> has been initialized for the current
        /// thread.
        /// </summary>
        new bool HasValue { get; }
    }
}
#endif