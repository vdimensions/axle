using System.Collections.Generic;

namespace Axle.Text
{
    public interface ITextDataObject : ITextDataNode
    {
        IEnumerable<ITextDataNode> GetChildren();
        IEnumerable<ITextDataNode> GetChildren(string name);
    }
}
