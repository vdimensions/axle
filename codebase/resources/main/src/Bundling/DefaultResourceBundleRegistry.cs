using System;
using System.Collections;
using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    public class DefaultResourceBundleRegistry : IResourceBundleRegistry
    {
        private sealed class BundleContentRegistry : IResourceBundleContentRegistry
        {
            public BundleContentRegistry()
            {
            }

            public IEnumerator<IResourceLocation> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IResourceBundleContentRegistry Register(IResourceLocation location)
            {
                throw new System.NotImplementedException();
            }

        }

        private readonly IDictionary<string, IResourceBundleContentRegistry> _perBundleContent;

        public DefaultResourceBundleRegistry(bool caseSensitiveBundleNames)
        {
            var comparer = caseSensitiveBundleNames ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            _perBundleContent = new Dictionary<string, IResourceBundleContentRegistry>(comparer);
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

        public IEnumerator<IResourceBundleContentRegistry> GetEnumerator()
        {
            return 
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IResourceBundleContentRegistry this[string bundle]
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}