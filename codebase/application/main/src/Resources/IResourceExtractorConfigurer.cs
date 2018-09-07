using Axle.Modularity;
using Axle.Resources.Extraction;


namespace Axle.Resources
{
    [Module]
    [RequiresResources]
    public interface IResourceExtractorConfigurer
    {
        void Configure(IResourceExtractorRegistry registry);
    }
}