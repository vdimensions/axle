namespace Axle.Caching
{
    public sealed class SimpleCacheManager : AbstractCacheManager
    {
        protected override ICache CreateCache(string cacheName)
        {
            return new SimpleCache();
        }
    }
}