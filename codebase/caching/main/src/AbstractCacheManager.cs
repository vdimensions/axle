#if NETSTANDARD1_1_OR_NEWER || NET35_OR_NEWER
using System;
using System.Collections.Concurrent;
using System.Linq;
using Axle.Verification;

namespace Axle.Caching
{
    /// <summary>
    /// An abstract class to serve as a base when implementing the <see cref="ICacheManager"/> interface.
    /// </summary>
    /// <seealso cref="ICacheManager"/>
    public abstract class AbstractCacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<string, ICache> _caches = new ConcurrentDictionary<string, ICache>(StringComparer.Ordinal); 

        void IDisposable.Dispose() => InvalidateAll();
        
        /// <inheritdoc />
        public ICache GetCache(string name) => _caches.GetOrAdd(name.VerifyArgument(nameof(name)).IsNotNull(), CreateCache);

        /// <inheritdoc />
        public void Invalidate(string name)
        {
            if (_caches.TryRemove(name.VerifyArgument(nameof(name)).IsNotNull(), out var cache))
            {
                cache.Evict();
            }
        }

        /// <inheritdoc />
        public void InvalidateAll()
        {
            foreach (var cache in _caches.Values.ToList())
            {
                cache.Evict();
            }
        }

        /// <summary>
        /// When overriden in a derived class, produces an instance of <see cref="ICache"/> that will be associated
        /// with the specified <paramref name="cacheName"/>.
        /// </summary>
        /// <param name="cacheName">
        /// The name of the cache to create a <see cref="ICache"/> instance for.
        /// </param>
        /// <returns>
        /// An <see cref="ICache"/> instance.
        /// </returns>
        protected abstract ICache CreateCache(string cacheName);
    }
}
#endif