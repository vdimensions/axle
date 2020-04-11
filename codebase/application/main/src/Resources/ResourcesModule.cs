using Axle.DependencyInjection;
using Axle.Modularity;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;


namespace Axle.Resources
{
    [Module]
    internal sealed class ResourcesModule : IResourceBundleConfigurer, IResourceExtractorConfigurer
    {
        private readonly ResourceManager _resourceManager = new DefaultResourceManager();

        [ModuleInit]
        internal void Init(IDependencyExporter exporter)
        {
            OnResourceBundleConfigurerInit(this);
            OnResourceExtractorConfigurerInit(this);

            exporter.Export(_resourceManager);
        }

        [ModuleDependencyInitialized]
        internal void OnResourceBundleConfigurerInit(IResourceBundleConfigurer configurer)
        {
            configurer.Configure(_resourceManager.Bundles);
        }

        [ModuleDependencyInitialized]
        internal void OnResourceExtractorConfigurerInit(IResourceExtractorConfigurer configurer)
        {
            configurer.Configure(_resourceManager.Extractors);
        }

        void IResourceBundleConfigurer.Configure(IResourceBundleRegistry registry) { }

        void IResourceExtractorConfigurer.Configure(IResourceExtractorRegistry registry) { }
    }
}
