#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.References
{
    /// <summary>
    /// Represents a generic weak reference, which references an object while still allowing that object to be reclaimed
    /// by garbage collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object that will be weakly referenced. This must be a reference type.
    /// </typeparam>
    /// <seealso cref="WeakReference"/>
    public interface IWeakReference<T> : IReference<T>, IEquatable<T>, IEquatable<IWeakReference<T>> where T: class
    {
        /// <summary>
        /// Gets or sets the object (the <see cref="Value">target</see>) referenced by the current
        /// <see cref="IWeakReference{T}">weak reference</see> instance.
        /// </summary>
        /// <returns>
        /// <c>null</c> if the object referenced by the current <see cref="IWeakReference{T}">weak reference</see>
        /// instance has been garbage collected; otherwise, a reference to the object referenced by the current
        /// <see cref="InvalidOperationException">weak reference</see> instance.
        /// </returns>
        /// <exception cref="System">
        /// The reference to the target object is invalid. <br />
        /// This exception can be thrown while setting this property if the value is a null reference or if the object
        /// has been finalized during the set operation.
        /// </exception>
        /// <seealso cref="WeakReference.Target"/>
        new T Value { get; set; }

        /// <summary>
        /// Gets an indication whether the object referenced by the current
        /// <see cref="IWeakReference{T}">weak reference</see> instance has been garbage collected.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the object referenced by the current <see cref="IWeakReference{T}">weak reference</see>
        /// instance has not been garbage collected and is still accessible; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="WeakReference.IsAlive"/>
        bool IsAlive { get; }
    }
}
#endif