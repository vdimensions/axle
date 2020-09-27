#if NETSTANDARD || NET35_OR_NEWER
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="IReadWriteLock"/> interface which acts as a
    /// wrapper to the <see cref="ReaderWriterLockSlim"/> class.
    /// <remarks>
    /// This implementation permits re-entrant access to the locked code block form the thread
    /// owning the lock. If you explicitly need to restrict recursive access from the thread
    /// owning the lock, use the non-reentrant <see cref="ReadWriteLock" /> class instead.
    /// </remarks>
    /// </summary>
    /// <seealso cref="ReadWriteLock"/>
    public sealed class ReentrantReadWriteLock : ReadWriteLock, IReentrantReadWriteLock
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ReentrantReadWriteLock"/> class.
        /// </summary>
        public ReentrantReadWriteLock() 
            // IMPORTANT: Earlier mono versions throw NotSupportedException for other than `NoRecursion` policies
            : base(new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)) {}
    }
}
#endif