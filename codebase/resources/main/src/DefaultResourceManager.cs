#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Extraction.Embedded;
using Axle.Resources.Extraction.FileSystem;
using Axle.Resources.Extraction.ResX;


namespace Axle.Resources
{
    public sealed class DefaultResourceManager : ResourceManager
    {
        public DefaultResourceManager()
            : base(new DefaultResourceBundleRegistry(), new DefaultResourceExtractorRegistry())
        {
            Extractors
                .Register(new EmbeddedResourceExtractor())
                .Register(new ResXResourceExtractor())
                .Register(new FileSystemResourceExtractor());
        }
    }
}
#endif