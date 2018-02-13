#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Marshalling;
using Axle.Verification;


namespace Axle.Resources
{
    public abstract class ResourceManager
    {
        protected ResourceManager(IResourceBundleRegistry bundles, IResourceMarshallerRegistry marshallers)
        {
            Bundles = bundles.VerifyArgument(nameof(bundles)).IsNotNull().Value;
            Marshallers = marshallers.VerifyArgument(nameof(marshallers)).IsNotNull().Value;
        }
        protected ResourceManager() 
            : this(new DefaultResourceBundleRegistry(), null) { }

        private IEnumerable<IResourceExtractor> GetExtractors(string bundle)
        {
            foreach (var location in Bundles[bundle])
            {
                yield return GetExtractor(location.Path);
            }
        }

        [Obsolete]
        public IEnumerable<ResourceInfo> ResolveAll(string bundle, string name, CultureInfo culture)
        {
            bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            foreach (var extractor in GetExtractors(bundle))
            {
                var res = extractor.Extract(name, culture);
                res.Bundle = bundle;
                yield return res;
            }
        }

        public ResourceInfo Resolve(string bundle, string name, CultureInfo culture)
        {
            return new BundleResourceExtractor(bundle, new ResourceExtractorChain(GetExtractors(bundle))).Extract(name, culture);
        }
        public T Resolve<T>(string bundle, string name, CultureInfo culture)
        {
            var extractor = new BundleResourceExtractor(bundle, new ResourceExtractorChain(GetExtractors(bundle)));
            var unmarshalled = (Marshallers ?? Enumerable.Empty<IResourceMarshaller>())
                .Select(x => x.TryUnmarshal(extractor, name, culture, typeof(T), out var result) ? result : null)
                .FirstOrDefault();
            return unmarshalled is T res ? res : throw new ResourceMarshallingException(name);
        }

        protected virtual IResourceExtractor GetExtractor(Uri path)
        {
            return new DefaultUriResourceExtractor(path);
        }

        public IResourceBundleRegistry Bundles { get; }
        public IResourceMarshallerRegistry Marshallers { get; }
    }
}
#endif