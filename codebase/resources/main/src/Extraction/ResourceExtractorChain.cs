using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public class ResourceExtractorChain : IResourceExtractorChain
    {
        private sealed class ChainCompositeResourceExtractor : IResourceExtractor
        {
            private readonly IResourceExtractor _a, _b;

            public ChainCompositeResourceExtractor(IResourceExtractor a, IResourceExtractor b)
            {
                _a = a;
                _b = b;
            }

            public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
            {
                if (_a is IResourceExtractorChain c)
                {
                    return c.TryExtract(location, name, culture, _b, out resource);
                }
                return _a.TryExtract(location, name, culture, out resource) || _b.TryExtract(location, name, culture, out resource);
            }
        }

        private readonly IResourceExtractor _next;

        public ResourceExtractorChain(IEnumerable<IResourceExtractor> extractors) : this(
                extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value.ToArray()) { }
        public ResourceExtractorChain(params IResourceExtractor[] extractors)
        {
            switch (extractors.Length)
            {
                case 0:
                    _next = null;
                    break;
                case 1:
                    _next = extractors[0];
                    break;
                default:
                    var collapsed = extractors[extractors.Length - 1];
                    for (var i = extractors.Length - 2; i >= 0; i--)
                    {
                        collapsed = new ChainCompositeResourceExtractor(collapsed, extractors[i]);
                    }
                    _next = collapsed;
                    break;
            }
        }

        public bool TryExtract(Uri location, string name, CultureInfo culture, out ResourceInfo resource)
        {
            resource = null;
            return _next != null && TryExtract(location, name, culture, _next, out resource);
        }
        public virtual bool TryExtract(Uri location, string name, CultureInfo culture, IResourceExtractor nextInChain, out ResourceInfo resource)
        {
            return nextInChain.TryExtract(location, name, culture, out resource);
        }
    }
}