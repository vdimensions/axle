namespace Axle.Threading
{
    #if NET20
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="IReadWriteLockProvider" />
    /// <seealso cref="ILock" />
    #elif NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="System.Threading.ReaderWriterLock" />
    /// <seealso cref="ReentrantReadWriteLockProvider" />
    /// <seealso cref="IReadWriteLockProvider" />
    /// <seealso cref="ILock" />
    #else
    /// <summary>
    /// An interface that provides the basis of a re-entrant reader-writer lock.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="ReentrantReadWriteLockProvider" />
    /// <seealso cref="IReadWriteLockProvider" />
    /// <seealso cref="ILock" />
    #endif
    public interface IReentrantReadWriteLockProvider : IReadWriteLockProvider {}
}