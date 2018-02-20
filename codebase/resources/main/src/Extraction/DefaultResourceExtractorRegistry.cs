using System.Collections;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public sealed class DefaultResourceExtractorRegistry : IResourceExtractorRegistry
    {
        private readonly LinkedList<IResourceExtractor> _extractors = new LinkedList<IResourceExtractor>();

        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator() { return _extractors.GetEnumerator(); }
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public IResourceExtractorRegistry Register(IResourceExtractor extractor)
        {
            _extractors.AddFirst(extractor.VerifyArgument(nameof(extractor)).IsNotNull().Value);
            return this;
        }
    }
}