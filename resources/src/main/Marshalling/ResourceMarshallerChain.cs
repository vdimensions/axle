using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Axle.Resources.Extraction;


namespace Axle.Resources.Marshalling
{
    [Obsolete]
    public class ResourceMarshallerChain : IResourceMarshaller
    {
        private readonly IResourceMarshaller _next;

        public ResourceMarshallerChain(IEnumerable<IResourceMarshaller> extractors) : this(extractors.ToArray()) { }
        public ResourceMarshallerChain(params IResourceMarshaller[] extractors)
        {
            if (extractors.Length == 0)
            {
                _next = new CompositeResourceMarshaller();
            }
            else if (extractors.Length == 1)
            {
                _next = extractors[0];
            }
            else
            {
                var collapsed = extractors[extractors.Length - 1];
                for (var i = extractors.Length - 2; i >= 0; i--)
                {
                    collapsed = new CompositeResourceMarshaller(extractors[i], collapsed);
                }
                _next = collapsed;
            }
        }

        public bool TryUnmarshal(IResourceExtractor extractor, string name, CultureInfo culture, Type targetType, out object result)
        {
            return TryUnmarshal(extractor, name, culture, targetType, out result, Next, this);
        }

        public virtual bool TryUnmarshal(
            IResourceExtractor extractor, 
            string name, 
            CultureInfo culture, 
            Type targetType, 
            out object result,
            IResourceMarshaller nextInChain,
            IResourceMarshaller outerChain)
        {
            if (nextInChain is ResourceMarshallerChain c)
            {
                return c.TryUnmarshal(extractor, name, culture, targetType, out result, c.Next, outerChain);
            }
            return nextInChain.TryUnmarshal(extractor, name, culture, targetType, out result);
        }

        protected IResourceMarshaller Next => _next;
    }
}