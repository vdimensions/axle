using System;

namespace Axle.Threading
{
    /// <summary>
    /// An <see langword="abstract"/> class to serve as a base when implementing the 
    /// <see cref="IReadWriteLockProvider"/> interface.
    /// </summary>
    public abstract class AbstractReadWriteLockProvider : IReadWriteLockProvider
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="AbstractReadWriteLockProvider"/>
        /// </summary>
        protected AbstractReadWriteLockProvider()
        {
            ReadLock = new ReadLock(this);
            UpgradeableReadLock = new UpgradeableReadLock(this);
            WriteLock = new WriteLock(this);
        }

        /// Enters the lock in read mode.
        public abstract void EnterReadLock();

        /// Enters the lock in upgradeable mode.
        public abstract void EnterUpgradeableReadLock();

        /// Enters the lock in write mode.
        public abstract void EnterWriteLock();

        /// Exits the current read mode lock.
        public abstract void ExitReadLock();

        /// Exits the current upgradeable mode lock.
        public abstract void ExitUpgradeableReadLock();

        /// Exits the current write mode lock.
        public abstract void ExitWriteLock();

        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or -1 (<see cref="System.Threading.Timeout.Infinite"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        public abstract bool TryEnterReadLock(int millisecondsTimeout);
        #if NETSTANDARD || NET45_OR_NEWER
        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        #else
        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        #endif
        public abstract bool TryEnterReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter the lock in upgradeable mode.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or -1 (<see cref="System.Threading.Timeout.Infinite"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered upgradeable mode, otherwise, <c>false</c>.
        /// </returns>
        public abstract bool TryEnterUpgradeableReadLock(int millisecondsTimeout);
        #if NETSTANDARD || NET45_OR_NEWER
        /// <summary>
        /// Tries to enter the lock in upgradeable mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered upgradeable mode, otherwise, <c>false</c>.
        /// </returns>
        #else
        /// <summary>
        /// Tries to enter the lock in upgradeable mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered upgradeable mode, otherwise, <c>false</c>.
        /// </returns>
        #endif
        public abstract bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or -1 (<see cref="System.Threading.Timeout.Infinite"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered write mode, otherwise, <c>false</c>.
        /// </returns>
        public abstract bool TryEnterWriteLock(int millisecondsTimeout);
        #if NETSTANDARD || NET45_OR_NEWER
        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>)
        /// to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered write mode, otherwise, <c>false</c>.
        /// </returns>
        #else
        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered write mode, otherwise, <c>false</c>.
        /// </returns>
        #endif
        public abstract bool TryEnterWriteLock(TimeSpan timeout);
        
        /// <inheritdoc/>
        public ReadLock ReadLock { get; }

        /// <inheritdoc/>
        public UpgradeableReadLock UpgradeableReadLock { get; }

        /// <inheritdoc/>
        public WriteLock WriteLock { get; } 
    }
}