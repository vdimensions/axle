using System;
using System.Linq;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using Axle.Reflection;
using Axle.Resources.Bundling;
using Axle.Resources.Configuration;
using Axle.Resources.Extraction;
using Axle.Resources.Properties.Extraction;
using Axle.Resources.Yaml.Extraction;

namespace Axle.Resources
{
    [Module]
    [ModuleConfigSection(typeof(ResourcesConfig), "Axle.Application.Resources")]
    internal sealed class ResourcesModule : IResourceBundleConfigurer, IResourceExtractorConfigurer
    {
        private readonly ResourceManager _resourceManager = new DefaultResourceManager();
        private readonly ResourcesConfig _config;

        public ResourcesModule() : this(new ResourcesConfig() { Bundles = new BundleConfig[0] }) { }
        public ResourcesModule(ResourcesConfig config)
        {
            _config = config;
        }

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

        void IResourceBundleConfigurer.Configure(IResourceBundleRegistry registry)
        {
            foreach (var bundleConfig in _config.Bundles.Union(new[]{_config.DefaultBundle}))
            {
                var bundleContent = registry.Configure(bundleConfig.Name);
                foreach (var location in bundleConfig.Locations)
                {
                    bundleContent.Register(location);
                }
                foreach (var extractorType in bundleConfig.Extractors)
                {
                    var extractorIntrospector = new TypeIntrospector(extractorType);
                    try
                    {
                        var extractorInstance = (IResourceExtractor) extractorIntrospector.CreateInstance();
                        bundleContent.Extractors.Register(extractorInstance);
                    }
                    catch (Exception e)
                    {
                        var message = $"Unable to create an instance of configured resource extractor type `{extractorType}`.";
                        Logger.Warn(message);
                        Logger.Debug(message, e);
                    }
                }
            }
        }

        void IResourceExtractorConfigurer.Configure(IResourceExtractorRegistry registry)
        {
            registry
                .Register(new PropertiesExtractor())
                .Register(new YamlExtractor());
        }
        
        internal ILogger Logger { get; set; }
    }
}
