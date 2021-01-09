#if NETSTANDARD1_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using Axle.Caching;
using Axle.Resources.Bundling;
#if NETSTANDARD1_6_OR_NEWER || NETFRAME
using Axle.Resources.Embedded.Extraction;
#endif
using Axle.Resources.Extraction;
#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using Axle.Resources.FileSystem.Extraction;
#endif
using Axle.Resources.ResX.Extraction;

namespace Axle.Resources
{
    #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
    /// <summary>
    /// The default implementation of the <see cref="ResourceManager"/> base class.
    /// It is capable of handling file system, resx and embedded resources in the given order,
    /// and can be further configured to deal with other resource extraction implementations.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="ResXResourceExtractor"/>
    /// <seealso cref="EmbeddedResourceExtractor"/>
    /// <seealso cref="FileSystemResourceExtractor"/>
    #elif NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    /// <summary>
    /// The default implementation of the <see cref="ResourceManager"/> base class.
    /// It is capable of handling file system, resx and embedded resources in the given order,
    /// and can be further configured to deal with other resource extraction implementations.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="ResXResourceExtractor"/>
    /// <seealso cref="FileSystemResourceExtractor"/>
    #else
    /// <summary>
    /// The default implementation of the <see cref="ResourceManager"/> base class.
    /// It is capable of handling file system, resx and embedded resources in the given order,
    /// and can be further configured to deal with other resource extraction implementations.
    /// </summary>
    /// <seealso cref="IResourceExtractor"/>
    /// <seealso cref="ResXResourceExtractor"/>
    #endif
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
                #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
                .Register(new FileSystemResourceExtractor())
                #endif
                .Register(new ResXResourceExtractor())
                #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
                .Register(new EmbeddedResourceExtractor())
                #endif
                ;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceManager"/> class.
        /// </summary>
        public DefaultResourceManager()
            #if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
            : this(new WeakReferenceCacheManager()) { }
            #else
            : this(null) { }
            #endif
    }
}
#endif