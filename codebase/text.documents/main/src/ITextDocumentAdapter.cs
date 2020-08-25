using System.Collections.Generic;

namespace Axle.Text.Documents
{
    public interface ITextDocumentAdapter
    {
        string Key { get; }
        CharSequence Value { get; }
        IEnumerable<ITextDocumentAdapter> Children { get; }
    }
}