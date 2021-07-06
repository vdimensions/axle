using Axle.Application;
using Axle.Caching;
using Axle.Resources.Extraction;

namespace Axle.Resources
{
    internal sealed class ApplicationResourceManager : ResourceManager
    {
        private readonly IApplicationHost _applicationHost;

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationResourceManager"/> class.
        /// </summary>
        /// <param name="applicationHost">
        /// A reference to the current application host instance.
        /// </param>
        /// <param name="cacheManager">
        /// An <see cref="ICacheManager"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation improving the performance of subsequently looked up resources.
        /// </param>
        public ApplicationResourceManager(IApplicationHost applicationHost, ICacheManager cacheManager) : base(cacheManager)
        {
            _applicationHost = applicationHost;
        }

        protected override IResourceExtractorRegistry ConfigureExtractors(IResourceExtractorRegistry extractors)
        {
            return _applicationHost.ConfigureDefaultResourcePaths(base.ConfigureExtractors(extractors));
        }
    }
}