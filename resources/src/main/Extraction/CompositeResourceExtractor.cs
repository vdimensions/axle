using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    internal sealed class CompositeResourceExtractor : IResourceExtractor
    {
        private readonly IEnumerable<IResourceExtractor> _extractors;

        public CompositeResourceExtractor(IEnumerable<IResourceExtractor> extractors)
        {
            _extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
        }

        public ResourceInfo Extract(string name, CultureInfo culture)
        {
            var result = _extractors.Select(x => x.Extract(name, culture)).FirstOrDefault(x => x != null);
            return result;
        }
    }
}