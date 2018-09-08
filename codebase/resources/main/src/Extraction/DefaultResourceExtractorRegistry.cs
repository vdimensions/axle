using System.Collections;
using System.Collections.Generic;

using Axle.References;
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
        public IResourceExtractorRegistry Register(Nullsafe<IResourceExtractor> extractor)
        {
            _extractors.AddFirst(extractor.VerifyRefArg(nameof(extractor)).IsNotNull().Value);
            return this;
        }
    }
}