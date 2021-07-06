using Axle.Modularity;
using Axle.Resources.Bundling;


namespace Axle.Resources
{
    /// <summary>
    /// An interface that enables an implementing module to configure the resource bundles of the global application
    /// resource manager.
    /// <remarks>
    /// This interface implies the effect of the <see cref="RequiresResourcesAttribute"/> on the implementing class
    /// if used as a module.
    /// </remarks>
    /// </summary>
    /// <seealso cref="IResourceBundleRegistry"/>
    /// <seealso cref="IResourceBundleContent"/>
    /// <seealso cref="RequiresResourcesAttribute"/>
    [Module]
    [RequiresResources]
    public interface IResourceBundleConfigurer
    {
        /// <summary>
        /// Configures the resource bundles of the the application resource manager.
        /// </summary>
        /// <param name="registry">
        /// A <see cref="IResourceBundleRegistry"/> object that facilitates the bundle configuration.
        /// </param>
        void Configure(IResourceBundleRegistry registry);
    }
}