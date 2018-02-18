using Axle.Resources.Bundling;


#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
namespace Axle.Resources
{
    using ResourceContextSplitStrategy = Axle.Resources.Extraction.ResourceContextSplitStrategy;

    public sealed class DefaultResourceManager : ResourceManager
    {
        public DefaultResourceManager()
            : base(new DefaultResourceBundleRegistry(), new Axle.Resources.Extraction.DefaultResourceExtractorRegistry(), null)
        {
            Extractors.Register(new Axle.Resources.Extraction.FileSystemResourceExtractor(ResourceContextSplitStrategy.ByCultureThenLocation));

            // ResX support varies depending on the platform
            #if !NETSTANDARD
            Extractors.Register(new Axle.Resources.Extraction.ResX.DefaultResXResourceExtractor());
            #elif NETSTANDARD2_0_OR_NEWER
            Extractors.Register(new Axle.Resources.Extraction.ResX.SimpleResXResourceExtractor());
            #elif NETSTANDARD1_3_OR_NEWER
            Extractors.Register(new Axle.Resources.Extraction.ResX.TextResXResourceExtractor());
            #endif

            Extractors.Register(new Axle.Resources.Extraction.EmbeddedResourceExtractor(ResourceContextSplitStrategy.ByCultureThenLocation));
        }
    }
}
#endif