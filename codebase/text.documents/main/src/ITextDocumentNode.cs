using System.Collections.Generic;

namespace Axle.Text.Documents
{
    public interface ITextDocumentNode
    {
        ITextDocumentObject Parent { get; }
        string Key { get; }
        IEqualityComparer<string> KeyComparer { get; }
    }
}
