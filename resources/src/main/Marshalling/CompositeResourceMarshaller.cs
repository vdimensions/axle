using System;
using System.Collections.Generic;
using System.Globalization;

using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources.Marshalling
{
    public sealed class CompositeResourceMarshaller : IResourceMarshaller
    {
        private readonly IEnumerable<IResourceMarshaller> _extractors;

        public CompositeResourceMarshaller(IEnumerable<IResourceMarshaller> extractors)
        {
            _extractors = extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value;
        }
        public CompositeResourceMarshaller(params IResourceMarshaller[] extractors) : this(extractors as IEnumerable<IResourceMarshaller>) { }

        public bool TryUnmarshal(IResourceExtractor extractor, string name, CultureInfo culture, Type targetType, out object result)
        {
            foreach (var resourceMarshaller in _extractors)
            {
                if (resourceMarshaller.TryUnmarshal(extractor, name, culture, targetType, out result))
                {
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}