using System.Collections.Generic;

namespace Axle.Text.Documents
{
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