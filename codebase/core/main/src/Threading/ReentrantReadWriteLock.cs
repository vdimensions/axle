using System.Threading;


namespace Axle.Threading
{
    /// <inheritdoc cref="ReadWriteLock" />
    public sealed class ReentrantReadWriteLock : ReadWriteLock, IReentrantReadWriteLock
    {
        // IMPORTANT: Earlier mono versions throw NotSupportedExcception for other than `NoRecursion` policies
        public ReentrantReadWriteLock() : base(new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)) {}
    }
}