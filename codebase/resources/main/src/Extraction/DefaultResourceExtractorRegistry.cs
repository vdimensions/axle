using System.Collections;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public sealed class DefaultResourceExtractorRegistry : IResourceExtractorRegistry
    {
        private readonly IList<IResourceExtractor> _extractors = new List<IResourceExtractor>();

        public IEnumerator<IResourceExtractor> GetEnumerator() { return _extractors.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IResourceExtractorRegistry Register(IResourceExtractor extractor)
        {
            _extractors.Add(extractor.VerifyArgument(nameof(extractor)).IsNotNull().Value);
            return this;
        }
    }
}