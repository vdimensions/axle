using System;


namespace Axle.Threading
{
    /// <summary>
    /// A <see cref="IDisposable">disposable</see> handle for a <see cref="ILock">synchronization lock</see> that is in place.
    /// </summary>
    /// <remarks>
    /// Disposal of this instance will release the held lock.
    /// </remarks>
    public interface ILockHandle : IDisposable
    {
        /// <summary>
        /// Gets a reference to the <see cref="ILock">lock object</see> representing the active synchronization lock.
        /// </summary>
        ILock Lock { get; }
    }
}