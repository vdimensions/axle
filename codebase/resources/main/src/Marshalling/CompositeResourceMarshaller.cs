using System;
using System.Collections.Generic;

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

        public bool TryUnmarshal(ResourceExtractionContext context, IResourceExtractor extractor, string name, Type targetType, out object result)
        {
            foreach (var resourceMarshaller in _extractors)
            {
                if (resourceMarshaller.TryUnmarshal(context, extractor, name, targetType, out result))
                {
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}