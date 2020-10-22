#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics;
using System.Threading;


namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="IReadWriteLock"/> interface which acts as a
    /// wrapper to the <see cref="ReaderWriterLockSlim"/> class.
    /// <remarks>
    /// This implementation does not permit re-entrant access to the locked code block form the thread
    /// owning the lock. For re-entrant lock implementation refer to the <see cref="ReentrantReadWriteLock"/> class.
    /// </remarks>
    /// </summary>
    /// <seealso cref="ReentrantReadWriteLock"/>
    public class ReadWriteLock : IDisposable, IReadWriteLock
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ReaderWriterLockSlim _innerLock;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool _supportsRecursion;

        /// <summary>
        /// Creates a new instance of the <see cref="ReadWriteLock"/> class without specifying lock recursion.
        /// </summary>
        public ReadWriteLock() : this(new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion)) { }
        internal ReadWriteLock(ReaderWriterLockSlim rls)
        {
            _innerLock = rls;
            _supportsRecursion = rls.RecursionPolicy == LockRecursionPolicy.SupportsRecursion;
        }

        void IDisposable.Dispose()
        {
            _innerLock.Dispose();
            _innerLock = null;
        }

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered read mode.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public void EnterReadLock() => _innerLock.EnterReadLock();

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered a lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter upgradeable
        /// mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public void EnterUpgradeableReadLock() => _innerLock.EnterUpgradeableReadLock();

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered the lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter the
        /// lock in write mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// The limit is so large that applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public void EnterWriteLock() => _innerLock.EnterWriteLock();

        /// <inheritdoc />
        public void ExitReadLock() => _innerLock.ExitReadLock();

        /// <inheritdoc />
        public void ExitUpgradeableReadLock() => _innerLock.ExitUpgradeableReadLock();

        /// <inheritdoc />
        public void ExitWriteLock() => _innerLock.ExitWriteLock();

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered the lock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterReadLock(int millisecondsTimeout) => _innerLock.TryEnterReadLock(millisecondsTimeout);
        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered the lock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterReadLock(TimeSpan timeout) => _innerLock.TryEnterReadLock(timeout);

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered the lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter the
        /// lock in write mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// The limit is so large that applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterWriteLock(int millisecondsTimeout) => _innerLock.TryEnterWriteLock(millisecondsTimeout);
        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered the lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter the
        /// lock in write mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// The limit is so large that applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterWriteLock(TimeSpan timeout) => _innerLock.TryEnterWriteLock(timeout);

        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered a lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter upgradeable
        /// mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout) => 
            _innerLock.TryEnterUpgradeableReadLock(millisecondsTimeout);
        /// <inheritdoc />
        /// <exception cref="System.Threading.LockRecursionException">
        /// <para>
        /// The <see cref="SupportsRecursion"/> property is set to <c>false</c>
        /// and the current thread has already entered a lock in any mode.
        /// </para>
        /// -or-
        /// <para>
        /// The current thread has entered read mode, so trying to enter upgradeable
        /// mode would create the possibility of a deadlock.
        /// </para>
        /// -or-
        /// <para>
        /// The recursion number would exceed the capacity of the counter.
        /// This limit is large enough so that an applications should never encounter it.
        /// </para>
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout) => _innerLock.TryEnterUpgradeableReadLock(timeout);

        #region Implementation of ILock
        /// <inheritdoc />
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public void Enter() => EnterWriteLock();

        /// <inheritdoc />
        public void Exit() => ExitWriteLock();

        /// <inheritdoc />
        /// <exception cref="ObjectDisposedException">
        /// The current <see cref="ReadWriteLock"/> instance has been disposed.
        /// </exception>
        public bool TryEnter(int millisecondsTimeout) => _innerLock.TryEnterWriteLock(millisecondsTimeout);

        /// <inheritdoc />
        public bool TryEnter(TimeSpan timeout) => _innerLock.TryEnterWriteLock(timeout);
        #endregion

        /// <summary>
        /// Gets a value that indicates if the current <see cref="ReaderWriterLockSlim"/> instance
        /// can enter a specific lock mode more than once.
        /// If this value is set to false, every attempt to obtain a lock when another lock of the
        /// same mode is already held will result in a <see cref="System.Threading.LockRecursionException"/>.
        /// </summary>
        public bool SupportsRecursion => _supportsRecursion;
    }
}
#endif