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
    internal sealed class DefaultResourceBundleRegistry : IResourceBundleRegistry
    {
        private readonly IDictionary<string, IConfigurableBundleContent> _perBundleContent;
        private readonly IResourceBundleFactory _bundleFactory;

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class.
        /// </summary>
        /// <param name="caseSensitiveBundleNames">
        /// A <see cref="bool">boolean</see> value specifying whether the resource bundle names should be case-sensitive.
        /// </param>
        public DefaultResourceBundleRegistry(bool caseSensitiveBundleNames, IResourceBundleFactory bundleFactory)
        {
            var comparer = caseSensitiveBundleNames ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            _perBundleContent = new ChronologicalDictionary<string, IConfigurableBundleContent>(comparer);
            _bundleFactory = bundleFactory;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultResourceBundleRegistry"/> class that is case-insensitive towards the resource bundle names.
        /// </summary>
        public DefaultResourceBundleRegistry(IResourceBundleFactory bundleFactory) : this(false, bundleFactory) { }

        /// <inheritdoc />
        public IConfigurableBundleContent Configure(string bundle)
        {
            if (!_perBundleContent.TryGetValue(bundle, out var contentRegistry))
            {
                _perBundleContent.Add(bundle, contentRegistry = _bundleFactory.CreateBundleContent(bundle));
            }
            return contentRegistry;
        }

        /// <inheritdoc />
        public IEnumerator<IResourceBundleContent> GetEnumerator() => _perBundleContent.Values.Cast<IResourceBundleContent>().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IResourceBundleContent this[string bundle]
        {
            get
            {
                var bundleContent = _perBundleContent.TryGetValue(bundle, out var result)
                    ? result
                    : _bundleFactory.CreateBundleContent(bundle);
                return new ReadOnlyResourceBundleContent(bundleContent);
            }
        }
    }
}