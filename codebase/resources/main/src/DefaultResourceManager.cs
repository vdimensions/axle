using Axle.Resources.Bundling;
using Axle.Resources.Extraction;


#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
namespace Axle.Resources
{
    public sealed class DefaultResourceManager : ResourceManager
    {
        public DefaultResourceManager()
            : base(new DefaultResourceBundleRegistry(), new DefaultResourceExtractorRegistry(), null)
        {
            Extractors.Register(new DefaultStreamableResourceExtractor(ResourceContextSplitStrategy.ByCultureThenLocation));

            // ResX support, depending on the platform
            #if !NETSTANDARD
            Extractors.Register(new Axle.Resources.Extraction.ResX.DefaultResXResourceExtractor());
            #elif NETSTANDARD2_0_OR_NEWER
            Extractors.Register(new Axle.Resources.Extraction.ResX.SimpleResXResourceExtractor());
            #elif NETSTANDARD1_3_OR_NEWER
            Extractors.Register(new Axle.Resources.Extraction.ResX.TextResXResourceExtractor());
            #endif
        }
    }
}
#endif