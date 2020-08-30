namespace Axle.Caching
{
    /// <summary>
    /// A <see cref="ICacheManager"/> implementation using <see cref="WeakReferenceCache"/> as an <see cref="ICache"/>
    /// implementation. 
    /// </summary>
    public sealed class WeakReferenceCacheManager : AbstractCacheManager
    {
        /// <summary>
        /// Creates a <see cref="WeakReferenceCache"/> instance for the provided <paramref name="cacheName"/>.
        /// </summary>
        /// <param name="cacheName">
        /// The cache name the returned <see cref="ICache"/> instance will be associated with.
        /// </param>
        /// <returns>
        /// A <see cref="WeakReferenceCache"/> instance.
        /// </returns>
        protected override ICache CreateCache(string cacheName) => new WeakReferenceCache();
    }
}