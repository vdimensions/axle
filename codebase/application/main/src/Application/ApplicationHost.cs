using Axle.Caching;
using Axle.Resources;

namespace Axle.Application
{
    /// <summary>
    /// A static class containing extension methods to the <see cref="IApplicationHost"/> interface.
    /// </summary>
    public static class ApplicationHost
    {
        /// <summary>
        /// Creates a <see cref="ResourceManager"/> instance with default paths configured by the application host.
        /// </summary>
        /// <param name="applicationHost">
        /// The <see cref="IApplicationHost"/> instance that will be used to configure the default paths for the
        /// resource manager.
        /// </param>
        /// <param name="cacheManager">
        /// An optional <see cref="ICacheManager"/> implementation to be used by the resource manager for
        /// resource caching.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceManager"/> instance that is configured to use paths as supported by the
        /// <paramref name="applicationHost"/> instance. 
        /// </returns>
        public static ResourceManager CreateResourceManager(this IApplicationHost applicationHost, ICacheManager cacheManager = null)
        {
            return new ApplicationResourceManager(applicationHost, cacheManager);
        }
    }
}