#if NETSTANDARD1_1_OR_NEWER || NET35_OR_NEWER
namespace Axle.Caching
{
    /// <summary>
    /// A <see cref="ICacheManager"/> implementation using <see cref="SimpleCache"/> as an <see cref="ICache"/>
    /// implementation. 
    /// </summary>
    public sealed class SimpleCacheManager : AbstractCacheManager
    {
        /// <summary>
        /// Creates a <see cref="SimpleCache"/> instance for the provided <paramref name="cacheName"/>.
        /// </summary>
        /// <param name="cacheName">
        /// The cache name the returned <see cref="ICache"/> instance will be associated with.
        /// </param>
        /// <returns>
        /// A <see cref="SimpleCache"/> instance.
        /// </returns>
        protected override ICache CreateCache(string cacheName) => new SimpleCache();
    }
}
#endif