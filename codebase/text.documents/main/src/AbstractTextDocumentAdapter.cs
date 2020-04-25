using System.Collections.Generic;

namespace Axle.Text.Documents
{
    public abstract class AbstractTextDocumentAdapter : ITextDocumentAdapter
    {
        public abstract string Key { get; }
        public abstract string Value { get; }
        public abstract IEnumerable<ITextDocumentAdapter> Children { get; }
    }
}