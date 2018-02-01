using System.Globalization;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public class BundleResourceExtractor : IResourceExtractor
    {
        private readonly string _bundle;
        private readonly IResourceExtractor _extractor;

        public BundleResourceExtractor(string bundle, IResourceExtractor extractor)
        {
            _bundle = bundle.VerifyArgument(nameof(bundle)).IsNotNull();
            _extractor = extractor;
        }

        public ResourceInfo Extract(string name, CultureInfo culture)
        {
            var result = _extractor.Extract(name, culture);
            result.Bundle = _bundle;
            return result;
        }
    }
}