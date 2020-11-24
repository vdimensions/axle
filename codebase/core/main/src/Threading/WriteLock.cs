using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// A wrapper around the <see cref="IReadWriteLockProvider"/> interface representing a write lock.
    /// </summary>
    public sealed class WriteLock : ILock
    {
        private readonly AbstractReadWriteLockProvider _lockProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteLock"/> class using the supplied
        /// <paramref name="lockProvider"/>.
        /// </summary>
        /// <param name="lockProvider">
        /// The <see cref="AbstractReadWriteLockProvider"/> instance to create the write locks.
        /// </param>
        public WriteLock(AbstractReadWriteLockProvider lockProvider)
        {
            _lockProvider = lockProvider;
        }
        
        /// <summary>
        /// Creates a <see cref="IDisposable">disposable</see> <see cref="WriteLockHandle"/> object. As a result,
        /// the current <see cref="ILock">lock</see> instance will acquire and keep a lock until the returned
        /// handle is disposed of.
        /// </summary>
        /// <returns>
        /// A <see cref="WriteLockHandle"/> object.
        /// </returns>
        /// <seealso cref="WriteLockHandle"/>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ILockHandle CreateHandle() => new WriteLockHandle(_lockProvider);

        /// <inheritdoc />
        public void Enter() => _lockProvider.EnterWriteLock();

        /// <inheritdoc />
        public void Exit() => _lockProvider.ExitWriteLock();

        /// <inheritdoc />
        public bool TryEnter(int millisecondsTimeout) => _lockProvider.TryEnterWriteLock(millisecondsTimeout);

        /// <inheritdoc />
        public bool TryEnter(TimeSpan timeout) => _lockProvider.TryEnterWriteLock(timeout);
    }
}