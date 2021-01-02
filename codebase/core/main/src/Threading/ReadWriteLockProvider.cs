#if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Threading;


namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="IReadWriteLockProvider"/> interface which acts as a
    /// wrapper to the <see cref="ReaderWriterLockSlim"/> class.
    /// <remarks>
    /// This implementation does not permit re-entrant access to the locked code block form the thread
    /// owning the lock. For re-entrant lock implementation refer to the <see cref="ReentrantReadWriteLockProvider"/> class.
    /// </remarks>
    /// </summary>
    /// <seealso cref="ReentrantReadWriteLockProvider"/>
    public sealed class ReadWriteLockProvider : IDisposable, IReadWriteLockProvider
    {
        private readonly ReadWriteLockSlimProvider _readWriteLockProvider = new ReadWriteLockSlimProvider(false);

        void IDisposable.Dispose() => ((IDisposable) _readWriteLockProvider)?.Dispose();
        
        /// <inheritdoc/>
        public ReadLock ReadLock => _readWriteLockProvider.ReadLock;

        /// <inheritdoc/>
        public UpgradeableReadLock UpgradeableReadLock => _readWriteLockProvider.UpgradeableReadLock;

        /// <inheritdoc/>
        public WriteLock WriteLock => _readWriteLockProvider.WriteLock;
    }
}
#endif