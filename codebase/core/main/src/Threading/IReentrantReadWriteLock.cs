namespace Axle.Threading
{
    #if NET20
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="IReadWriteLock" />
    /// <seealso cref="ILock" />
    #elif NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="System.Threading.ReaderWriterLock" />
    /// <seealso cref="ReentrantReadWriteLock" />
    /// <seealso cref="IReadWriteLock" />
    /// <seealso cref="ILock" />
    #else
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="ReentrantReadWriteLock" />
    /// <seealso cref="IReadWriteLock" />
    /// <seealso cref="ILock" />
    #endif
    public interface IReentrantReadWriteLock : IReadWriteLock {}
}