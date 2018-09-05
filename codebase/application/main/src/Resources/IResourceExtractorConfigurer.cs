using Axle.Modularity;
using Axle.Resources.Extraction;


namespace Axle.Resources
{
    [Module]
    public interface IResourceExtractorConfigurer : IResourcesDependency
    {
        void Configure(IResourceExtractorRegistry registry);
    }
}