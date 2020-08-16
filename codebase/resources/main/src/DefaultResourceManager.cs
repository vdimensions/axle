#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using Axle.Caching;
using Axle.Resources.Bundling;
using Axle.Resources.Embedded.Extraction;
using Axle.Resources.Extraction;
using Axle.Resources.FileSystem.Extraction;
using Axle.Resources.ResX.Extraction;

namespace Axle.Resources
{
    /// <summary>
    /// The default implementation of the <see cref="ResourceManager"/> base class.
    /// It is capable of handling file system, resx and embedded resources in the given order,
    /// and can be further configured to deal with other resource extraction implementations.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="FileSystemResourceExtractor"/>
    /// <seealso cref="ResXResourceExtractor"/>
    /// <seealso cref="EmbeddedResourceExtractor"/>
    public sealed class DefaultResourceManager : ResourceManager
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceManager"/> class.
        /// </summary>
        /// <param name="cacheManager">
        /// An <see cref="ICacheManager"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation improving the performance of subsequently looked up resources.
        /// </param>
        public DefaultResourceManager(ICacheManager cacheManager) 
            : base(
                new DefaultResourceBundleRegistry(), 
                new DefaultResourceExtractorRegistry(), 
                cacheManager)
        {
            Extractors
                .Register(new FileSystemResourceExtractor())
                .Register(new ResXResourceExtractor())
                .Register(new EmbeddedResourceExtractor());
        }
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceManager"/> class.
        /// </summary>
        public DefaultResourceManager() : this(new WeakReferenceCacheManager()) { }
    }
}
#endif