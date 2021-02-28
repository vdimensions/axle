#if NETSTANDARD1_0_OR_NEWER || NETFRAMEWORK
using System;
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
        private sealed class ConfigurableResourceBundleFactory : ResourceBundleFactoryDecorator
        {
            private sealed class ConfigurableBundleContent : IConfigurableBundleContent
            {
                private readonly IConfigurableBundleContent _impl;
                private readonly IResourceExtractorRegistry _extractors;

                public ConfigurableBundleContent(IConfigurableBundleContent impl, ResourceManager resourceManager)
                {
                    _impl = impl;
                    _extractors = resourceManager.ConfigureExtractors(_impl.Extractors);
                }

                public IConfigurableBundleContent Register(Uri location)
                {
                    _impl.Register(location);
                    return this;
                }

                public IResourceExtractorRegistry Extractors => _extractors;
                IEnumerable<IResourceExtractor> IResourceBundleContent.Extractors => _extractors;
                public IEnumerable<Uri> Locations => _impl.Locations;
                string IResourceBundleContent.Bundle => _impl.Bundle;
            }
            
            private readonly ResourceManager _resourceManager;

            public ConfigurableResourceBundleFactory(IResourceBundleFactory impl, ResourceManager resourceManager) 
                : base(impl) 
                => _resourceManager = resourceManager;

            public override IConfigurableBundleContent CreateBundleContent(string bundle)
            {
                return new ConfigurableBundleContent(base.CreateBundleContent(bundle), _resourceManager);
            }
        }
        
        private sealed class ConfigurableResourceExtractorRegistry : ResourceExtractorRegistryDecorator
        {
            private readonly ResourceManager _resourceManager;

            public ConfigurableResourceExtractorRegistry(IResourceExtractorRegistry impl, ResourceManager resourceManager) 
                : base(impl)
            {
                _resourceManager = resourceManager;
            }

            public override IResourceExtractorRegistry Register(IResourceExtractor extractor)
            {
                return _resourceManager.ConfigureExtractors(base.Register(extractor));
            }
        }
        
        #if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
        private sealed class CacheAwareResourceBundleFactory : ResourceBundleFactoryDecorator
        {
            private sealed class CacheAwareConfigurableBundleContent : IConfigurableBundleContent
            {
                private readonly string _bundle;
                private readonly IConfigurableBundleContent _impl;
                private readonly ICacheManager _cacheManager;

                public CacheAwareConfigurableBundleContent(string bundle, IConfigurableBundleContent impl, ICacheManager cacheManager)
                {
                    _bundle = bundle;
                    _impl = impl;
                    _cacheManager = cacheManager;
                }

                public IConfigurableBundleContent Register(Uri location)
                {
                    var result = _impl.Register(location);
                    _cacheManager.Invalidate(_bundle);
                    return new CacheAwareConfigurableBundleContent(_bundle, result, _cacheManager);
                }

                public IResourceExtractorRegistry Extractors => _impl.Extractors;
                IEnumerable<IResourceExtractor> IResourceBundleContent.Extractors => ((IResourceBundleContent) _impl).Extractors;
                public IEnumerable<Uri> Locations => _impl.Locations;
                string IResourceBundleContent.Bundle => _impl.Bundle;
            }
            
            private readonly ICacheManager _cacheManager;

            public CacheAwareResourceBundleFactory(IResourceBundleFactory impl, ICacheManager cacheManager) : base(impl)
            {
                _cacheManager = cacheManager;
            }

            public override IConfigurableBundleContent CreateBundleContent(string bundle)
            {
                return new CacheAwareConfigurableBundleContent(bundle, base.CreateBundleContent(bundle), _cacheManager);
            }
        }
        
        private sealed class CacheAwareResourceExtractorRegistry : ResourceExtractorRegistryDecorator
        {
            private readonly IEnumerable<IResourceBundleContent> _bundles;
            private readonly ICacheManager _cacheManager;

            public CacheAwareResourceExtractorRegistry(
                IResourceExtractorRegistry impl, 
                IEnumerable<IResourceBundleContent> bundles, 
                ICacheManager cacheManager) : base(impl)
            {
                _bundles = bundles;
                _cacheManager = cacheManager;
            }

            public override IResourceExtractorRegistry Register(IResourceExtractor extractor)
            {
                var result = base.Register(extractor);
                foreach (var bundle in _bundles)
                {
                    _cacheManager.Invalidate(bundle.Bundle);
                }
                return new CacheAwareResourceExtractorRegistry(result, _bundles, _cacheManager);
            }
        }

        private sealed class CachingExtractor : AbstractResourceExtractor
        {
            private readonly IResourceExtractor _impl;
            private readonly ICache _cache;
            private readonly int _index;

            public CachingExtractor(IResourceExtractor impl, ICache cache, int index)
            {
                _impl = impl;
                _cache = cache;
                _index = index;
            }

            protected override ResourceInfo DoExtract(IResourceContext context, string name)
            {
                var key = Tuple.Create(name, _index, context.Culture.Name, context.Location);
                return _cache.GetOrAdd(key, k => _impl.Extract(context, k.Item1));
            }
        }

        private static IResourceExtractor CreateCachingExtractor(IResourceExtractor extractor, ICache cache, int index) 
            => new CachingExtractor(extractor, cache, index);

        private readonly ICacheManager _cacheManager;
        #endif
        
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="bundleFactory"/> and <paramref name="extractors"/>.
        /// </summary>
        /// <param name="bundleFactory">
        /// The <see cref="IResourceBundleFactory"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation.
        /// </param>
        /// <param name="extractors">
        /// The <see cref="IResourceExtractorRegistry"/> instance to be used by the current
        /// <see cref="ResourceManager"/> implementation.
        /// </param>
        private ResourceManager(IResourceBundleFactory bundleFactory, IResourceExtractorRegistry extractors)
            : this(bundleFactory, extractors, null) { }
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="bundleFactory"/> and <paramref name="extractors"/>.
        /// </summary>
        /// <param name="bundleFactory">
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
        private ResourceManager(
                IResourceBundleFactory bundleFactory, 
                IResourceExtractorRegistry extractors, 
                ICacheManager cacheManager)
        {
            bundleFactory.VerifyArgument(nameof(bundleFactory)).IsNotNull();
            extractors.VerifyArgument(nameof(extractors)).IsNotNull();
            IResourceBundleFactory factory = new ConfigurableResourceBundleFactory(bundleFactory, this);
            #if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
            if ((_cacheManager = cacheManager) != null)
            {
                factory = new CacheAwareResourceBundleFactory(factory, _cacheManager);
                Bundles = new DefaultResourceBundleRegistry(factory);
                Extractors = new CacheAwareResourceExtractorRegistry(new ConfigurableResourceExtractorRegistry(extractors, this), Bundles, _cacheManager);
            }
            else
            {
                Bundles = new DefaultResourceBundleRegistry(factory);
                Extractors = new ConfigurableResourceExtractorRegistry(extractors, this);
            }
            #else
            Bundles = new DefaultResourceBundleRegistry(factory);
            Extractors = new ConfigurableResourceExtractorRegistry(extractors, this);
            #endif
        }
        
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation with the provided
        /// <paramref name="cacheManager"/>.
        /// </summary>
        /// <param name="cacheManager">
        /// An <see cref="ICacheManager"/> instance to be used by the current <see cref="ResourceManager"/>
        /// implementation improving the performance of subsequently looked up resources.
        /// </param>
        protected ResourceManager(ICacheManager cacheManager) 
            : this(DefaultResourceBundleFactory.Instance, new DefaultResourceExtractorRegistry(), cacheManager) { }
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceManager"/> implementation.
        /// </summary>
        protected ResourceManager() 
            : this(DefaultResourceBundleFactory.Instance, new DefaultResourceExtractorRegistry(), null) { }
        
        protected virtual IResourceExtractorRegistry ConfigureExtractors(IResourceExtractorRegistry extractors) 
            => extractors;

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

            var resourceBundle = Bundles[bundle];
            var locations = resourceBundle.Locations.ToArray();
            if (locations.Length == 0)
            {
                return null;
            }
            #if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
            var cache = _cacheManager?.GetCache(bundle);
            var extractors = (cache == null
                ? /*Extractors.Union*/(resourceBundle.Extractors)
                : /*Extractors.Union*/(resourceBundle.Extractors).Select((e, i) => CreateCachingExtractor(e, cache, i)))
            #else
            var extractors = /*Extractors
                .Union*/(resourceBundle.Extractors)
            #endif
                .Reverse()
                .ToArray();
            
            foreach (var ci in culture.ExpandHierarchy())
            {
                #if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
                var context = cache == null
                    ? ResourceContext.Create(resourceBundle, ci)
                    : cache.GetOrAdd<Tuple<string, Type>, IResourceContext>(
                        Tuple.Create(ci.Name, typeof(ResourceContext)), 
                        t => ResourceContext.Create(resourceBundle, ci));
                var result = cache == null
                    ? context.Extract(name)
                    : cache.GetOrAdd(
                        Tuple.Create(name, ci.Name, typeof(ResourceInfo)), 
                        t => context.Extract(t.Item1));
                #else
                var result = ResourceContext.Create(resourceBundle, culture).Extract(name);
                #endif
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