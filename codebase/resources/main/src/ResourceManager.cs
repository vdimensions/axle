#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System.Globalization;
using System.Linq;

using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Resources.Marshalling;
using Axle.Verification;


namespace Axle.Resources
{
    public abstract partial class ResourceManager
    {
        protected ResourceManager(IResourceBundleRegistry bundles, IResourceExtractorRegistry extractors, IResourceMarshallerRegistry marshallers)
        {
            Bundles = bundles.VerifyArgument(nameof(bundles)).IsNotNull().Value;
            Extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
            Marshallers = marshallers.VerifyArgument(nameof(marshallers)).IsNotNull().Value;
        }

        public ResourceInfo Resolve(string bundle, string name, CultureInfo culture)
        {
            bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            var bundleExtractor = new BundleResourceExtractor(bundle.VerifyArgument(nameof(bundle)).IsNotNull(), new ResourceExtractorChain(Extractors));
            foreach (var location in Bundles[bundle])
            {
                if (bundleExtractor.TryExtract(location, name, culture, out var result))
                {
                    return result;
                }
            }
            return null;
        }
        public T Resolve<T>(string bundle, string name, CultureInfo culture)
        {
            var bundleExtractor = new BundleResourceExtractor(bundle.VerifyArgument(nameof(bundle)).IsNotNull(), new ResourceExtractorChain(Extractors));
            var unmarshalled = (Marshallers ?? Enumerable.Empty<IResourceMarshaller>())
                .Select(x => x.TryUnmarshal(bundleExtractor, name, culture, typeof(T), out var result) ? result : null)
                .FirstOrDefault();
            return unmarshalled is T res ? res : throw new ResourceMarshallingException(name);
        }

        public IResourceBundleRegistry Bundles { get; }
        public IResourceExtractorRegistry Extractors { get; }
        public IResourceMarshallerRegistry Marshallers { get; }
    }
}
#endif