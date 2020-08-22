using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Caching
{
    public abstract class AbstractCacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<string, ICache> _caches = new ConcurrentDictionary<string, ICache>(StringComparer.Ordinal); 

        void IDisposable.Dispose() => InvalidateAll();
        
        public ICache GetCache(string name) => _caches.GetOrAdd(name.VerifyArgument(nameof(name)).IsNotNull(), CreateCache);

        public void Invalidate(string name)
        {
            if (_caches.TryRemove(name.VerifyArgument(nameof(name)).IsNotNull(), out var cache))
            {
                cache.Evict();
            }
        }

        public void InvalidateAll()
        {
            foreach (var cachesKey in _caches.Keys.ToList())
            {
                Invalidate(cachesKey);
            }
        }

        protected abstract ICache CreateCache(string cacheName);
        
        public IEnumerable<string> Caches => _caches.Keys;
    }
}