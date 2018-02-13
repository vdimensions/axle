using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Axle.Collections;


namespace Axle.Resources.Bundling
{
    public sealed class DefaultResourceBundleRegistry : IResourceBundleRegistry
    {
        private sealed class BundleContentRegistry : IResourceBundleContentRegistry
        {
            private readonly LinkedList<Uri> _locations;

            public BundleContentRegistry()
            {
                _locations = new LinkedList<Uri>();
            }

            public IEnumerator<Uri> GetEnumerator() => _locations.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IResourceBundleContentRegistry Register(Uri location)
            {
                _locations.AddLast(location);
                return this;
            }
        }

        private readonly IDictionary<string, IResourceBundleContentRegistry> _perBundleContent;

        public DefaultResourceBundleRegistry(bool caseSensitiveBundleNames)
        {
            var comparer = caseSensitiveBundleNames ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            _perBundleContent = new ChronologicalDictionary<string, IResourceBundleContentRegistry>(comparer);
        }
        public DefaultResourceBundleRegistry() : this(false) { }

        public IResourceBundleContentRegistry Configure(string bundle)
        {
            if (!_perBundleContent.TryGetValue(bundle, out var contentRegistry))
            {
                _perBundleContent.Add(bundle, contentRegistry = new BundleContentRegistry());
            }
            return contentRegistry;
        }

        public IEnumerator<IResourceBundleContentRegistry> GetEnumerator() => _perBundleContent.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<Uri> this[string bundle] => _perBundleContent.TryGetValue(bundle, out var result) ? result : Enumerable.Empty<Uri>();
    }
}