using Axle.Modularity;
using Axle.Resources.Bundling;


namespace Axle.Resources
{
    [Module]
    [RequiresResources]
    public interface IResourceBundleConfigurer
    {
        void Configure(IResourceBundleRegistry registry);
    }
}