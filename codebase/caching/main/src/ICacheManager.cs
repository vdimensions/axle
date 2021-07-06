using System;

namespace Axle.Caching
{
    #if NETSTANDARD1_1_OR_NEWER || NET35_OR_NEWER
    /// <summary>
    /// An interface that represents a cache manager. The cache manager is responsible for providing
    /// <see cref="ICache">cache objects</see> identified by a name, as well as invalidating such caches.  
    /// </summary>
    /// <seealso cref="AbstractCacheManager"/>
    #else
    /// <summary>
    /// An interface that represents a cache manager. The cache manager is responsible for providing
    /// <see cref="ICache">cache objects</see> identified by a name, as well as invalidating such caches.  
    /// </summary>
    #endif
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Obtains the <see cref="ICache"/> instance that corresponds to the provided <paramref name="name" />. In case
        /// the respective cache does not yet exist within the current <see cref="ICacheManager"/>, a new empty cache is
        /// created. 
        /// </summary>
        /// <param name="name">
        /// The name of the cache to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="ICache"/> instance that corresponds to the provided <paramref name="name" /> within the current
        /// <see cref="ICacheManager"/> instance.
        /// </returns>
        ICache GetCache(string name);

        /// <summary>
        /// Invalidates all cached entries for the <see cref="ICache"/> object that corresponds to the provided
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="ICache"/> to invalidate.
        /// </param>
        void Invalidate(string name);
        
        /// <summary>
        /// Invalidates all cached entries for caches maintained by the current <see cref="ICacheManager"/> instance.
        /// </summary>
        void InvalidateAll();
    }
}