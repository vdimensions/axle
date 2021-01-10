#if NETSTANDARD || NET20_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
namespace Axle.Threading
{
    #if NET20
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="ILock" />
    #elif NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="System.Threading.ReaderWriterLock" />
    /// <seealso cref="ReadWriteLockProvider" />
    /// <seealso cref="ILock" />
    #else
    /// <summary>
    /// An interface that provides the basis of a reader-writer lock; that is
    /// an object which utilizes synchronization mechanisms for read and write access to another object or resource.
    /// Multiple threads can read from the resource at a time, but only one can modify it.
    /// All reader threads will be blocked if a writer thread is currently engaging the resource.
    /// </summary>
    /// <seealso cref="System.Threading.ReaderWriterLockSlim" />
    /// <seealso cref="ReadWriteLockProvider" />
    /// <seealso cref="ILock" />
    #endif
    public interface IReadWriteLockProvider
    {
        /// <summary>
        /// Gets a reference to the read lock provided by the current <see cref="ReadLock"/> instance.
        /// </summary>
        ReadLock ReadLock { get; }
        
        /// <summary>
        /// Gets a reference to the upgradeable read lock provided by the current <see cref="UpgradeableReadLock"/> instance.
        /// </summary>
        UpgradeableReadLock UpgradeableReadLock { get; }
        
        /// <summary>
        /// Gets a reference to the write lock provided by the current <see cref="WriteLock"/> instance.
        /// </summary>
        WriteLock WriteLock { get; }
    }
}
#endif