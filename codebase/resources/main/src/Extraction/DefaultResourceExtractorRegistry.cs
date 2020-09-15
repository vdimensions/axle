using System.Collections;
using System.Collections.Generic;
using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// The default implementation of the <see cref="IResourceExtractorRegistry"/> interface.
    /// </summary>
    public sealed class DefaultResourceExtractorRegistry : IResourceExtractorRegistry
    {
        private readonly LinkedList<IResourceExtractor> _extractors = new LinkedList<IResourceExtractor>();

        /// <inheritdoc />
        IEnumerator<IResourceExtractor> IEnumerable<IResourceExtractor>.GetEnumerator() => _extractors.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _extractors.GetEnumerator();

        /// <inheritdoc />
        public IResourceExtractorRegistry Register(IResourceExtractor extractor)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(extractor, nameof(extractor)));
            _extractors.AddLast(extractor);
            return this;
        }
    }
}