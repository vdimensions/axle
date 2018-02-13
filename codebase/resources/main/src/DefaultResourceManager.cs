using Axle.Resources.Bundling;
using Axle.Resources.Extraction;


#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
namespace Axle.Resources
{
    public sealed class DefaultResourceManager : ResourceManager
    {
        public DefaultResourceManager() 
            : base(new DefaultResourceBundleRegistry(), new DefaultResourceExtractorRegistry(), null) { }
    }
}
#endif