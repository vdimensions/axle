using System.Collections.Generic;

namespace Axle.Text.Documents
{
    public interface ITextDocumentObject : ITextDocumentNode
    {
        IEnumerable<ITextDocumentNode> GetChildren();
        IEnumerable<ITextDocumentNode> GetChildren(string name);
    }
}
