using System;
using System.Collections.Generic;
using System.Globalization;

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

        public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            foreach (var e in _extractors)
            {
                if (e.TryExtract(location, name, culture, out resource))
                {
                    return true;
                }
            }
            resource = null;
            return false;
        }
    }
}