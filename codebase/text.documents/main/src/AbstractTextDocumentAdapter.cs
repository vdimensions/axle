using System.Collections.Generic;

namespace Axle.Text.Documents
{
    /// <summary>
    /// An abstract class to serve as a base for implementing the <see cref="ITextDocumentAdapter"/> interface.
    /// </summary>
    public abstract class AbstractTextDocumentAdapter : ITextDocumentAdapter
    {
        /// <inheritdoc />
        public abstract string Key { get; }
        /// <inheritdoc />
        public abstract CharSequence Value { get; }
        /// <inheritdoc />
        public abstract IEnumerable<ITextDocumentAdapter> Children { get; }
    }
}