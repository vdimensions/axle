using System.Collections.Generic;

namespace Axle.Text.Data
{
    public interface ITextDataObject : ITextDataNode
    {
        IEnumerable<ITextDataNode> GetChildren();
        IEnumerable<ITextDataNode> GetChildren(string name);
    }
}
