using System;


namespace Axle.Threading
{
    /// <summary>
    /// A <see cref="IDisposable">disposable</see> handle that represents an active
    /// <see cref="ILock">synchronization lock</see> that is used to control the lock duration.
    /// </summary>
    /// <remarks>
    /// Disposal of this instance will release the held lock.
    /// </remarks>
    public interface ILockHandle : IDisposable { }
}