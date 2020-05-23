using Axle.Modularity;
using Axle.Resources.Bundling;


namespace Axle.Resources
{
    /// <summary>
    /// An interface that enables an implementing module to configure the resource bundles of the application
    /// resource manager.
    /// This interface introduces an implicit dependency on the <see cref="ResourcesModule"/>.  
    /// </summary>
    /// <seealso cref="IResourceBundleRegistry"/>
    /// <seealso cref="IResourceBundleContent"/>
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