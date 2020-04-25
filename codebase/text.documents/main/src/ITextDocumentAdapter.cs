using System.Collections.Generic;

namespace Axle.Text.Documents
{
    public interface ITextDocumentAdapter
    {
        string Key { get; }
        string Value { get; }
        IEnumerable<ITextDocumentAdapter> Children { get; }
    }
}