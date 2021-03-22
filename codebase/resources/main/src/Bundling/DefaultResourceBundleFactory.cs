using System;
using System.Collections.Generic;
#if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
using Axle.References;
#endif
using Axle.Resources.Extraction;

namespace Axle.Resources.Bundling
{
    internal sealed class DefaultResourceBundleFactory : IResourceBundleFactory
    {
        private sealed class ConfigurableBundleContent : IConfigurableBundleContent
        {
            private readonly LinkedList<Uri> _locations;
            private readonly IResourceExtractorRegistry _extractors;
            private readonly string _bundle;

            public ConfigurableBundleContent(string bundle)
            {
                _bundle = bundle;
                _locations = new LinkedList<Uri>();
                _extractors = new DefaultResourceExtractorRegistry();
            }

            IConfigurableBundleContent IConfigurableBundleContent.Register(Uri location)
            {
                // Prevent issues if registering the same location twice in a row
                if (_locations.Count == 0 || !location.Equals(_locations.First.Value))
                {
                    _locations.AddFirst(location);
                }
                return this;
            }

            IResourceExtractorRegistry IConfigurableBundleContent.Extractors => _extractors;

            IEnumerable<Uri> IResourceBundleContent.Locations => _locations;
            IEnumerable<IResourceExtractor> IResourceBundleContent.Extractors => _extractors;
            string IResourceBundleContent.Bundle => _bundle;
        }

        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
        public static IResourceBundleFactory Instance => Singleton<DefaultResourceBundleFactory>.Instance.Value;
        #else
        public static readonly IResourceBundleFactory Instance = new DefaultResourceBundleFactory();
        #endif
        
        private DefaultResourceBundleFactory() { }

        public IConfigurableBundleContent CreateBundleContent(string bundle) => new ConfigurableBundleContent(bundle);
    }
}