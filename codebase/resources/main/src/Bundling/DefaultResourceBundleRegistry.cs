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
        private sealed class BundleContent : IResourceBundleContent
        {
            private readonly LinkedList<Uri> _locations;
            private readonly DefaultResourceExtractorRegistry _extractors;

            public BundleContent()
            {
                _locations = new LinkedList<Uri>();
                _extractors = new DefaultResourceExtractorRegistry();
            }

            public IEnumerator<Uri> GetEnumerator() => _locations.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IResourceBundleContent Register(Uri location)
            {
                _locations.AddLast(location);
                return this;
            }

            public IResourceExtractorRegistry Extractors => _extractors;
        }

        private readonly IDictionary<string, IResourceBundleContent> _perBundleContent;

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class.
        /// </summary>
        /// <param name="caseSensitiveBundleNames">
        /// A <see cref="bool">boolean</see> value specifying whether the resource bundle names should be case-sensitive.
        /// </param>
        public DefaultResourceBundleRegistry(bool caseSensitiveBundleNames)
        {
            var comparer = caseSensitiveBundleNames ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            _perBundleContent = new ChronologicalDictionary<string, IResourceBundleContent>(comparer);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class that is case-insensitive towards the resource bundle names.
        /// </summary>
        public DefaultResourceBundleRegistry() : this(false) { }

        /// <inheritdoc />
        public IResourceBundleContent Configure(string bundle)
        {
            if (!_perBundleContent.TryGetValue(bundle, out var contentRegistry))
            {
                _perBundleContent.Add(bundle, contentRegistry = new BundleContent());
            }
            return contentRegistry;
        }

        /// <inheritdoc />
        public IEnumerator<IResourceBundleContent> GetEnumerator() => _perBundleContent.Values.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IEnumerable<Uri> this[string bundle] => _perBundleContent.TryGetValue(bundle, out var result) ? result : Enumerable.Empty<Uri>();
    }
}