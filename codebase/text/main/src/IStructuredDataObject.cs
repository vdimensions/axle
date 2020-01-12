using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataObject : IStructuredDataNode
    {
        IEnumerable<IStructuredDataNode> GetChildren();
        IEnumerable<IStructuredDataNode> GetChildren(string name);
    }
}
