using Axle.Modularity;
using Axle.Resources.Bundling;


namespace Axle.Resources
{
    [Module]
    [Requires(typeof(ResourcesModule))]
    public interface IResourceBundleConfigurer
    {
        void Configure(IResourceBundleRegistry registry);
    }
}