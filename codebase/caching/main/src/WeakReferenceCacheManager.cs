namespace Axle.Caching
{
    public sealed class WeakReferenceCacheManager : AbstractCacheManager
    {
        protected override ICache CreateCache(string cacheName) => new WeakReferenceCache();
    }
}