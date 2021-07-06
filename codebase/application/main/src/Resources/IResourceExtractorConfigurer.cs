using Axle.Modularity;
using Axle.Resources.Extraction;


namespace Axle.Resources
{
    /// <summary>
    /// An interface that enables an implementing module to configure the resource extractors of the application
    /// resource manager.
    /// This interface introduces an implicit dependency on the <see cref="ResourcesModule"/>.  
    /// </summary>
    /// <seealso cref="IResourceExtractorRegistry"/>
    /// <seealso cref="IResourceExtractor"/>
    [Module]
    [RequiresResources]
    public interface IResourceExtractorConfigurer
    {
        /// <summary>
        /// Configures additional <see cref="IResourceExtractor">resource extractor</see> implementations to be
        /// available to the application resource manager.
        /// </summary>
        /// <param name="registry">
        /// A <see cref="IResourceExtractorRegistry"/> object that is used to register additional resource extractors
        /// with the application resource manager.
        /// </param>
        void Configure(IResourceExtractorRegistry registry);
    }
}