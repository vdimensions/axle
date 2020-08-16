using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.Collections;
using Axle.Resources.Extraction;

namespace Axle.Resources.Bundling
{
    /// <summary>
    /// The default implementation of the <see cref="IResourceBundleRegistry"/> interface.
    /// </summary>
    public sealed class DefaultResourceBundleRegistry : IResourceBundleRegistry
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
                _locations.AddFirst(location);
                return this;
            }

            IResourceExtractorRegistry IConfigurableBundleContent.Extractors => _extractors;

            IEnumerable<Uri> IResourceBundleContent.Locations => _locations;
            IEnumerable<IResourceExtractor> IResourceBundleContent.Extractors => _extractors;
            string IResourceBundleContent.Bundle => _bundle;
        }

        private readonly IDictionary<string, IConfigurableBundleContent> _perBundleContent;

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class.
        /// </summary>
        /// <param name="caseSensitiveBundleNames">
        /// A <see cref="bool">boolean</see> value specifying whether the resource bundle names should be case-sensitive.
        /// </param>
        public DefaultResourceBundleRegistry(bool caseSensitiveBundleNames)
        {
            var comparer = caseSensitiveBundleNames ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            _perBundleContent = new ChronologicalDictionary<string, IConfigurableBundleContent>(comparer);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class that is case-insensitive towards the resource bundle names.
        /// </summary>
        public DefaultResourceBundleRegistry() : this(false) { }

        /// <inheritdoc />
        public IConfigurableBundleContent Configure(string bundle)
        {
            if (!_perBundleContent.TryGetValue(bundle, out var contentRegistry))
            {
                _perBundleContent.Add(bundle, contentRegistry = new ConfigurableBundleContent(bundle));
            }
            return contentRegistry;
        }

        /// <inheritdoc />
        public IEnumerator<IResourceBundleContent> GetEnumerator() => _perBundleContent.Values.Cast<IResourceBundleContent>().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IResourceBundleContent this[string bundle] => 
            _perBundleContent.TryGetValue(bundle, out var result) 
                ? result 
                : new ConfigurableBundleContent(bundle);
    }
}