using System;


namespace Axle.Threading
{
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="System.Threading.ReaderWriterLock" />
    /// <seealso cref="ILock" />
    public interface IReadWriteLock : ILock
    {
        void EnterReadLock();

        void EnterUpgradeableReadLock();

        void EnterWriteLock();

        void ExitReadLock();

        void ExitUpgradeableReadLock();

        void ExitWriteLock();

        bool TryEnterReadLock(int millisecondsTimeout);
        bool TryEnterReadLock(TimeSpan timeout);

        bool TryEnterUpgradeableReadLock(int millisecondsTimeout);
        bool TryEnterUpgradeableReadLock(TimeSpan timeout);

        bool TryEnterWriteLock(int millisecondsTimeout);
        bool TryEnterWriteLock(TimeSpan timeout);
    }
}
