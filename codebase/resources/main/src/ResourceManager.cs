﻿#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Axle.Caching;
using Axle.Globalization.Extensions.CultureInfo;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Verification;

namespace Axle.Resources
{
    /// <summary>
    /// An abstract class to serve as a base for a resource manager; that is, an object 
    /// that allows convenient access at runtime for a wide range of resources, including 
    /// culture-specific data and embedded objects.
    /// </summary>
    public abstract class ResourceManager
    {
        private sealed class CacheAwareBundleRegistry : IResourceBundleRegistry
        {
            private sealed class CacheAwareConfigurableBundleContent : IConfigurableBundleContent
            {
                private readonly IConfigurableBundleContent _impl;
                private readonly ICacheManager _cacheManager;

                public CacheAwareConfigurableBundleContent(IConfigurableBundleContent impl, ICacheManager cacheManager)
                {
                    _impl = impl;
                    _cacheManager = cacheManager;
                }

                public IConfigurableBundleContent Register(Uri location)
                {
                    var result = _impl.Register(location);
                    foreach (var cache in _cacheManager.Caches)
                    {
                        _cacheManager.Invalidate(cache);
                    }
                    return new CacheAwareConfigurableBundleContent(result, _cacheManager);
                }

                public IResourceExtractorRegistry Extractors => _impl.Extractors;
                IEnumerable<IResourceExtractor> IResourceBundleContent.Extractors => ((IResourceBundleContent) _impl).Extractors;
                public IEnumerable<Uri> Locations => _impl.Locations;
            }
            
            private readonly IResourceBundleRegistry _impl;
            private readonly ICacheManager _cacheManager;

            public CacheAwareBundleRegistry(IResourceBundleRegistry impl, ICacheManager cacheManager)
            {
                _impl = impl;
                _cacheManager = cacheManager;
            }

            public IEnumerator<IResourceBundleContent> GetEnumerator() => _impl.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _impl).GetEnumerator();

            public IConfigurableBundleContent Configure(string bundle) 
                => new CacheAwareConfigurableBundleContent(_impl.Configure(bundle), _cacheManager);

            public IResourceBundleContent this[string bundle] => _impl[bundle];
        }
        
        private sealed class CacheAwareResourceExtractorRegistry : IResourceExtractorRegistry
        {
            private readonly IResourceExtractorRegistry _impl;
            private readonly ICacheManager _cacheManager;

            public CacheAwareResourceExtractorRegistry(IResourceExtractorRegistry impl, ICacheManager cacheManager)
            {
                _impl = impl;
                _cacheManager = cacheManager;
            }

            public IEnumerator<IResourceExtractor> GetEnumerator() => _impl.GetEnumerator();
            
            public IResourceExtractorRegistry Register(IResourceExtractor extractor)
            {
                var result = _impl.Register(extractor);
                foreach (var cache in _cacheManager.Caches)
                {
                    _cacheManager.Invalidate(cache);
                }
                return new CacheAwareResourceExtractorRegistry(result, _cacheManager);
            }

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _impl).GetEnumerator();
        }
        
        private readonly ICacheManager _cacheManager;
        
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="bundles"/> and <paramref name="extractors"/>.
        /// </summary>
        /// <param name="bundles">
        /// The <see cref="IResourceBundleRegistry"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation.
        /// </param>
        /// <param name="extractors">
        /// The <see cref="IResourceExtractorRegistry"/> instance to be used by the current
        /// <see cref="ResourceManager"/> implementation.
        /// </param>
        protected ResourceManager(IResourceBundleRegistry bundles, IResourceExtractorRegistry extractors)
            : this(bundles, extractors, null) { }
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="bundles"/> and <paramref name="extractors"/>.
        /// </summary>
        /// <param name="bundles">
        /// The <see cref="IResourceBundleRegistry"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation.
        /// </param>
        /// <param name="extractors">
        /// The <see cref="IResourceExtractorRegistry"/> instance to be used by the current
        /// <see cref="ResourceManager"/> implementation.
        /// </param>
        /// <param name="cacheManager">
        /// An <see cref="ICacheManager"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation improving the performance of subsequently looked up resources.
        /// </param>
        protected ResourceManager(
                IResourceBundleRegistry bundles, 
                IResourceExtractorRegistry extractors, 
                ICacheManager cacheManager)
        {
            bundles.VerifyArgument(nameof(bundles)).IsNotNull();
            extractors.VerifyArgument(nameof(extractors)).IsNotNull();
            if ((_cacheManager = cacheManager) != null)
            {
                Bundles = new CacheAwareBundleRegistry(bundles, _cacheManager);
                Extractors = new CacheAwareResourceExtractorRegistry(extractors, _cacheManager);
            }
            else
            {
                Bundles = bundles;
                Extractors = extractors;
            }
        }

        /// <summary>
        /// Attempts to resolve a resource object based on the provided parameters.
        /// </summary>
        /// <param name="bundle">
        /// The name of the resource bundle where the resource is contained.
        /// </param>
        /// <param name="name">
        /// The name of the resource to locate.
        /// </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/> instance representing the culture for which the resource is being requested.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResourceInfo"/> that represents the loaded resource object.
        /// </returns>
        public ResourceInfo Load(string bundle, string name, CultureInfo culture)
        {
            bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            culture.VerifyArgument(nameof(culture)).IsNotNull();

            var bundleRegistry = Bundles[bundle];
            var locations = bundleRegistry.Locations.ToArray();
            if (locations.Length == 0)
            {
                return null;
            }
            var extractors = bundleRegistry.Extractors.Union(Extractors).ToArray();
            foreach (var ci in culture.ExpandHierarchy())
            {
                var context = new ResourceContext(bundle, locations, ci, extractors);
                var cache = _cacheManager?.GetCache(ci.Name);
                var result =  cache == null
                    ? context.ExtractionChain.Extract(name)
                    : cache.GetOrAdd(Tuple.Create(bundle, name), t => context.ExtractionChain.Extract(((Tuple<string, string>) t).Item2));
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Attempts to resolve a resource object based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object that is represented by the loaded resource.
        /// </typeparam>
        /// <param name="bundle">
        /// The name of the resource bundle where the resource is contained.
        /// </param>
        /// <param name="name">
        /// The name of the resource to locate.
        /// </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/> instance representing the culture for which the resource is being requested.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/>, which is resolved from the loaded resource.
        /// </returns>
        public T Load<T>(string bundle, string name, CultureInfo culture)
        {
            var resource = Load(bundle, name, culture);
            if (resource != null && resource.TryResolve<T>(out var instance))
            {
                return instance;
            }
            return default(T);
        }

        /// <summary>
        /// Exposes the <see cref="IResourceBundleRegistry">resource bundle registry</see> that
        /// contains the list of locations which the current <see cref="ResourceManager"/> will use
        /// internally to resolve resources for a given bundle.
        /// </summary>
        public IResourceBundleRegistry Bundles { get; }

        /// <summary>
        /// Exposes the <see cref="IResourceExtractorRegistry">resource extractor registry</see> that
        /// contains the resource extractors which the current <see cref="ResourceManager"/> will use
        /// internally when resolving resources.
        /// </summary>
        public IResourceExtractorRegistry Extractors { get; }
    }
}
#endif