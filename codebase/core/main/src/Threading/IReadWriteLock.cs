using System;


namespace Axle.Threading
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="System.Threading.ReaderWriterLock" />
    /// <seealso cref="ReadWriteLock" />
    /// <seealso cref="ILock" />
    #else
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="ReadWriteLock" />
    /// <seealso cref="ILock" />
    #endif
    public interface IReadWriteLock : ILock
    {
        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        void EnterReadLock();

        /// <summary>
        /// Tries to enter the lock in upgradeable mode.
        /// </summary>
        void EnterUpgradeableReadLock();

        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        void EnterWriteLock();

        /// <summary>
        /// Reduces the recursion count for read mode, and exits read mode if the resulting count is <c>0</c> (zero).
        /// </summary>
        /// <exception cref="System.Threading.SynchronizationLockException">
        /// The current thread has not entered the lock in read mode.
        /// </exception>
        void ExitReadLock();

        /// <summary>
        /// Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is <c>0</c> (zero).
        /// </summary>
        /// <exception cref="System.Threading.SynchronizationLockException">
        /// The current thread has not entered the lock in upgradeable mode.
        /// </exception>
        void ExitUpgradeableReadLock();

        /// <summary>
        /// Reduces the recursion count for write mode, and exits write mode if the resulting count is <c>0</c> (zero).
        /// </summary>
        /// <exception cref="System.Threading.SynchronizationLockException">
        /// The current thread has not entered the lock in write mode.
        /// </exception>
        void ExitWriteLock();

        /// <summary>
        /// Tries to enter the lock in read mode, with an optional integer time-out.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="millisecondsTimeout"/> is negative, but it is not equal to 
        /// <see cref="System.Threading.Timeout.Infinite"/> (<c>-1</c>), which is the only negative value allowed. 
        /// </exception>
        bool TryEnterReadLock(int millisecondsTimeout);
        /// <summary>
        /// Tries to enter the lock in read mode, with an optional time-out.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or <c>-1</c> milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>) to wait indefinitely. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="timeout"/> is negative, but it is not equal to <c>-1</c> milliseconds, 
        /// which is the only negative value allowed.
        /// -or-
        /// The value of <paramref name="timeout"/> is greater than <see cref="System.Int32.MaxValue"/> milliseconds. 
        /// </exception>
        bool TryEnterReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter the lock in upgradeable mode, with an optional integer time-out.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="millisecondsTimeout"/> is negative, but it is not equal to 
        /// <see cref="System.Threading.Timeout.Infinite"/> (<c>-1</c>), which is the only negative value allowed. 
        /// </exception>
        bool TryEnterUpgradeableReadLock(int millisecondsTimeout);
        /// <summary>
        /// Tries to enter the lock in upgradeable mode, with an optional time-out.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or <c>-1</c> milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>) to wait indefinitely. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="timeout"/> is negative, but it is not equal to <c>-1</c> milliseconds, 
        /// which is the only negative value allowed.
        /// -or-
        /// The value of <paramref name="timeout"/> is greater than <see cref="System.Int32.MaxValue"/> milliseconds. 
        /// </exception>
        bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        /// <summary>
        /// Tries to enter the lock in write mode, with an optional integer time-out.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <c>-1</c> (<see cref="System.Threading.Timeout.Infinite"/>) to wait indefinitely.
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="millisecondsTimeout"/> is negative, but it is not equal to 
        /// <see cref="System.Threading.Timeout.Infinite"/> (<c>-1</c>), which is the only negative value allowed. 
        /// </exception>
        bool TryEnterWriteLock(int millisecondsTimeout);
        /// <summary>
        /// Tries to enter the lock in write mode, with an optional time-out.
        /// </summary>
        /// <param name="timeout">
        /// The interval to wait, or <c>-1</c> milliseconds (<see cref="System.Threading.Timeout.InfiniteTimeSpan"/>) to wait indefinitely. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the calling thread entered read mode, otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="timeout"/> is negative, but it is not equal to <c>-1</c> milliseconds, 
        /// which is the only negative value allowed.
        /// -or-
        /// The value of <paramref name="timeout"/> is greater than <see cref="System.Int32.MaxValue"/> milliseconds. 
        /// </exception>
        bool TryEnterWriteLock(TimeSpan timeout);
    }
}
