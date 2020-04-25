using System;

namespace Axle.Text.Documents
{
    public interface ITextDocumentNode
    {
        ITextDocumentObject Parent { get; }
        string Key { get; }
        StringComparer KeyComparer { get; }
    }
}
