#if NETSTANDARD || NET35_OR_NEWER
using System.Threading;


namespace Axle.Threading
{
    /// <inheritdoc cref="ReadWriteLock" />
    public sealed class ReentrantReadWriteLock : ReadWriteLock, IReentrantReadWriteLock
    {
        // IMPORTANT: Earlier mono versions throw NotSupportedException for other than `NoRecursion` policies
        public ReentrantReadWriteLock() : base(new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)) {}
    }
}
#endif