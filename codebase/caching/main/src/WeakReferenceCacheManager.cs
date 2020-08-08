using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Caching
{
    public sealed class WeakReferenceCacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<string, ICache> _caches = new ConcurrentDictionary<string, ICache>(StringComparer.Ordinal); 

        public ICache GetCache(string name) => _caches.GetOrAdd(name.VerifyArgument(nameof(name)).IsNotNull(), x => new WeakReferenceCache());

        public void Invalidate(string name) => _caches.TryRemove(name.VerifyArgument(nameof(name)).IsNotNull(), out var _);
        
        public IEnumerable<string> Caches => _caches.Keys;
    }
}