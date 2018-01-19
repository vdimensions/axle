using System;
using System.Diagnostics;
using System.Threading;


namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="IReadWriteLock"/> interface which acts as a
    /// wrapper to the <see cref="System.Threading.ReaderWriterLockSlim"/> class.
    /// </summary>
    public class ReadWriteLock : IDisposable, IReadWriteLock
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ReaderWriterLockSlim innerLock;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool supportsRecursion;

        public ReadWriteLock() : this(new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion)) { }
        internal ReadWriteLock(ReaderWriterLockSlim rls)
        {
            innerLock = rls;
        }

        void IDisposable.Dispose()
        {
            innerLock.Dispose();
            innerLock = null;
        }

        /// <summary>
        /// Enters the lock in read mode.
        /// </summary>
        /// <exception cref="System.Threading.LockRecursionException">
        /// The <see cref="SupportsRecursion"/> property is false and the current thread has already entered read mode. 
        /// -or- 
        /// The recursion number would exceed the capacity of the counter. 
        /// This limit is large enough so that an applications should never encounter it. 
        /// </exception>
        public void EnterReadLock() { innerLock.EnterReadLock(); }

        public void EnterUpgradeableReadLock() { innerLock.EnterUpgradeableReadLock(); }

        /// <summary>
        /// Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero).
        /// </summary>
        /// <exception cref="System.Threading.SynchronizationLockException">
        /// The current thread has not entered the lock in read mode.
        /// </exception>
        public void EnterWriteLock() { innerLock.EnterWriteLock(); }

        public void ExitReadLock() { innerLock.ExitReadLock(); }

        public void ExitUpgradeableReadLock() { innerLock.ExitUpgradeableReadLock(); }

        public void ExitWriteLock() { innerLock.ExitWriteLock(); }

        public bool TryEnterReadLock(int millisecondsTimeout) { return innerLock.TryEnterReadLock(millisecondsTimeout); }
        public bool TryEnterReadLock(TimeSpan timeout) { return innerLock.TryEnterReadLock(timeout); }

        public bool TryEnterWriteLock(int millisecondsTimeout) { return innerLock.TryEnterWriteLock(millisecondsTimeout); }
        public bool TryEnterWriteLock(TimeSpan timeout) { return innerLock.TryEnterWriteLock(timeout); }

        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout) { return innerLock.TryEnterUpgradeableReadLock(millisecondsTimeout); }
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout) { return innerLock.TryEnterUpgradeableReadLock(timeout); }

        #region Implementation of ILock
        public void Enter() { EnterWriteLock(); }

        public void Exit() { ExitWriteLock(); }

        public bool TryEnter(int millisecondsTimeout) { return innerLock.TryEnterWriteLock(millisecondsTimeout); }
        public bool TryEnter(TimeSpan timeout) { return innerLock.TryEnterWriteLock(timeout); }
        #endregion

        /// <summary>
        /// Gets a value that indicates if the current <see cref="ReaderWriterLockSlim"/> instance
        /// can enter a specific lock mode more than once.
        /// If this value is set to false, every attempt to obtain a lock when another lock of the 
        /// same mode is alreaddy held will result in a <see cref="System.Threading.LockRecursionException"/>.
        /// </summary>
        public bool SupportsRecursion { get { return supportsRecursion; } }
    }
}