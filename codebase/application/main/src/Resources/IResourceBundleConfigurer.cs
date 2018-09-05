using Axle.Modularity;
using Axle.Resources.Bundling;


namespace Axle.Resources
{
    [Module]
    public interface IResourceBundleConfigurer : IResourcesDependency
    {
        void Configure(IResourceBundleRegistry registry);
    }
}