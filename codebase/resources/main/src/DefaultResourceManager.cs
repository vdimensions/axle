#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Extraction.Embedded;
using Axle.Resources.Extraction.FileSystem;
using Axle.Resources.Extraction.ResX;


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
        public DefaultResourceManager() : base(new DefaultResourceBundleRegistry(), new DefaultResourceExtractorRegistry())
        {
            Extractors
                .Register(new EmbeddedResourceExtractor())
                .Register(new ResXResourceExtractor())
                .Register(new FileSystemResourceExtractor());
        }
    }
}
#endif