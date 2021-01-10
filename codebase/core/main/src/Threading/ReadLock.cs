#if NETSTANDARD || NET20_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// A wrapper around the <see cref="IReadWriteLockProvider"/> interface representing a read lock.
    /// </summary>
    public sealed class ReadLock : ILock
    {
        private readonly AbstractReadWriteLockProvider _lockProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadLock"/> class using the supplied
        /// <paramref name="lockProvider"/>.
        /// </summary>
        /// <param name="lockProvider">
        /// The <see cref="AbstractReadWriteLockProvider"/> instance to create the read locks.
        /// </param>
        public ReadLock(AbstractReadWriteLockProvider lockProvider)
        {
            _lockProvider = lockProvider;
        }
        
        /// <summary>
        /// Creates a <see cref="IDisposable">disposable</see> <see cref="ReadLockHandle"/> object. As a result,
        /// the current <see cref="ILock">lock</see> instance will acquire and keep a lock until the returned
        /// handle is disposed of.
        /// </summary>
        /// <returns>
        /// A <see cref="ReadLockHandle"/> object.
        /// </returns>
        /// <seealso cref="ReadLockHandle"/>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public ILockHandle CreateHandle() => new ReadLockHandle(_lockProvider);

        /// <inheritdoc />
        public void Enter() => _lockProvider.EnterReadLock();

        /// <inheritdoc />
        public void Exit() => _lockProvider.ExitReadLock();

        /// <inheritdoc />
        public bool TryEnter(int millisecondsTimeout) => _lockProvider.TryEnterReadLock(millisecondsTimeout);

        /// <inheritdoc />
        public bool TryEnter(TimeSpan timeout) => _lockProvider.TryEnterReadLock(timeout);
    }
}
#endif