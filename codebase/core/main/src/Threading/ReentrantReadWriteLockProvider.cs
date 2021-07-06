#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="IReadWriteLockProvider"/> interface which acts as a
    /// wrapper to the <see cref="ReaderWriterLockSlim"/> class.
    /// <remarks>
    /// This implementation permits re-entrant access to the locked code block form the thread
    /// owning the lock. If you explicitly need to restrict recursive access from the thread
    /// owning the lock, use the non-reentrant <see cref="ReadWriteLockProvider" /> class instead.
    /// </remarks>
    /// </summary>
    /// <seealso cref="ReadWriteLockProvider"/>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class ReentrantReadWriteLockProvider : IDisposable, IReentrantReadWriteLockProvider
    {
        private readonly ReadWriteLockSlimProvider _readWriteLockProvider = new ReadWriteLockSlimProvider(true);

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