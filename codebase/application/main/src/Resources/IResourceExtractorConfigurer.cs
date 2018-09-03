using Axle.Modularity;
using Axle.Resources.Extraction;


namespace Axle.Resources
{
    [Module]
    [Requires(typeof(ResourcesModule))]
    public interface IResourceExtractorConfigurer
    {
        void Configure(IResourceExtractorRegistry registry);
    }
}