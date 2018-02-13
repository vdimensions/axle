using System.Collections.Generic;
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

            public ResourceInfo Extract(ResourceExtractionContext context, string name)
            {
                if (_a is IResourceExtractorChain c)
                {
                    return c.Extract(context, name, _b);
                }
                return _a.Extract(context, name) ?? _b.Extract(context, name);
            }
        }

        private readonly IResourceExtractor _next;

        public ResourceExtractorChain(IEnumerable<IResourceExtractor> extractors) 
            : this(extractors.VerifyArgument(nameof(extractors)).IsNotNull().Value.ToArray()) { }
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

        //protected virtual ResourceInfo DoExtract(ResourceExtractionContext contex, string name, IResourceExtractor nextInChain) => null;

        public ResourceInfo Extract(ResourceExtractionContext context, string name) => _next != null ? Extract(context, name, _next) : null;
        public virtual ResourceInfo Extract(ResourceExtractionContext context, string name, IResourceExtractor nextInChain)
        {
            //var items = context.Extract(
            //    name,
            //    (location, culture, n) =>
            //    {
            //        //var currentResult = DoExtract(context, n, nextInChain);
            //        //if (currentResult != null)
            //        //{
            //        //    return currentResult;
            //        //}
            //        var tmpContext = new ResourceExtractionContext(
            //            context.Bundle,
            //            context.LookupLocations.Except(new[]{location}), 
            //            culture);
            //        return nextInChain.Extract(tmpContext, n);
            //    });
            //return items.SingleOrDefault();
            return nextInChain.Extract(context, name);
        }
    }
}
/**
 *  |______________
 *  | ?GreetingText
 *  V
 * [chain]->[properties]->[embedded resources]->[resx]->[filesystem]
 * [en-us]           (1)
 *                    |_________________
 *                    | ?some.properties
 *                    V
 *               [chain]->[embedded resources]->[resx]->[filesystem]
 *               [en-us]                   (3)      4            (5) 
 *          -----------------------------------------------------------
 *    [en]->[properties]->[embedded resources]->[resx]->[filesystem]
 *      []          
 *                 
 *                 
 */
