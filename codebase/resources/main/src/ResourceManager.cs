#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using System.Globalization;
using System.Linq;

using Axle.Extensions.Globalization.CultureInfo;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources
{
    public abstract partial class ResourceManager
    {
        protected ResourceManager(IResourceBundleRegistry bundles, IResourceExtractorRegistry extractors)
        {
            Bundles = bundles.VerifyArgument(nameof(bundles)).IsNotNull().Value;
            Extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
        }

        public ResourceInfo Resolve(string bundle, string name, CultureInfo culture)
        {
            bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            var bundles = Bundles[bundle].ToArray();
            var extractors = Extractors.ToArray();
            foreach (var ci in culture.ExpandHierarchy())
            {
                var context = new ResourceContext(bundle, bundles, ci, extractors);
                var result = context.ExtractionChain.Extract(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public IResourceBundleRegistry Bundles { get; }
        public IResourceExtractorRegistry Extractors { get; }
    }
}
#endif