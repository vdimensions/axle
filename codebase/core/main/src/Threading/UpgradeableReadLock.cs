using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// A wrapper around the <see cref="IReadWriteLockProvider"/> interface representing an upgradeable read lock.
    /// </summary>
    public sealed class UpgradeableReadLock : ILock
    {
        private readonly AbstractReadWriteLockProvider _lockProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeableReadLock"/> class using the supplied
        /// <paramref name="lockProvider"/>.
        /// </summary>
        /// <param name="lockProvider">
        /// The <see cref="AbstractReadWriteLockProvider"/> instance to create the upgradeable locks.
        /// </param>
        public UpgradeableReadLock(AbstractReadWriteLockProvider lockProvider)
        {
            _lockProvider = lockProvider;
        }
        
        /// <summary>
        /// Creates a <see cref="IDisposable">disposable</see> <see cref="UpgradeableReadLockHandle"/> object. As a
        /// result, the current <see cref="ILock">lock</see> instance will acquire and keep a lock until the returned
        /// handle is disposed of.
        /// </summary>
        /// <returns>
        /// A <see cref="UpgradeableReadLockHandle"/> object.
        /// </returns>
        /// <seealso cref="UpgradeableReadLockHandle"/>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ILockHandle CreateHandle() => new UpgradeableReadLockHandle(_lockProvider);

        /// <inheritdoc />
        public void Enter() => _lockProvider.EnterUpgradeableReadLock();

        /// <inheritdoc />
        public void Exit() => _lockProvider.ExitUpgradeableReadLock();

        /// <inheritdoc />
        public bool TryEnter(int millisecondsTimeout) => _lockProvider.TryEnterUpgradeableReadLock(millisecondsTimeout);

        /// <inheritdoc />
        public bool TryEnter(TimeSpan timeout) => _lockProvider.TryEnterUpgradeableReadLock(timeout);
    }
}