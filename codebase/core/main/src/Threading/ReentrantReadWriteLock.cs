#if NETSTANDARD || NET35_OR_NEWER
using System.Threading;


namespace Axle.Threading
{
    /// <inheritdoc cref="ReadWriteLock" />
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