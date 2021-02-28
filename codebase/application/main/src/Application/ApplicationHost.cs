using Axle.Caching;
using Axle.Resources;

namespace Axle.Application
{
    public static class ApplicationHost
    {
        public static ResourceManager CreateResourceManager(this IApplicationHost applicationHost, ICacheManager cacheManager = null)
        {
            return new ApplicationResourceManager(applicationHost, cacheManager);
        }
    }
}