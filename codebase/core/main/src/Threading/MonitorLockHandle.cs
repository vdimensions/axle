using System;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Threading
{
    /// <summary>
    /// A <see cref="ILockHandle"/> implementation that is provided for a <see cref="MonitorLock"/>.
    /// </summary>
    public sealed class MonitorLockHandle : ILockHandle
    {
        private readonly MonitorLock _lock;

        internal MonitorLockHandle(MonitorLock @lock)
        {
            _lock = @lock;
            @lock.Enter();
        }

        /// <summary>
        /// Releases the lock held by the <see cref="MonitorLock"/> object that owns the current
        /// <see cref="MonitorLockHandle">lock handle</see> and blocks the current thread until
        /// it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue.
        /// </summary>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> representing the amount of time to wait before the thread enters the ready queue.
        /// </param>
        /// <returns>
        /// <c>true</c> if the lock was reacquired before the specified time elapsed;
        /// <c>false</c> if the lock was reacquired after the specified time elapsed.
        /// The method does not return until the lock is reacquired.
        /// </returns>
        public bool Wait(TimeSpan timeout) => _lock.Wait(timeout);
        /// <summary>
        /// Releases the lock held by the <see cref="MonitorLock"/> object that owns the current
        /// <see cref="MonitorLockHandle">lock handle</see> and blocks the current thread until
        /// it reacquires the lock.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the call returned because the caller reacquired the lock for the specified object.
        /// This method does not return if the lock is not reacquired.
        /// </returns>
        public bool Wait() => _lock.Wait();
        
        /// <summary>
        /// Notifies a thread in the waiting queue of a change in the lock's state.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Pulse() => _lock.Pulse();
        
        /// <summary>
        /// Notifies all waiting threads of a change in the lock's state.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void PulseAll() => _lock.PulseAll();

        /// <inheritdoc cref="IDisposable.Dispose()"/>
        public void Dispose() => _lock?.Exit();
        void IDisposable.Dispose() => Dispose();
    }
}