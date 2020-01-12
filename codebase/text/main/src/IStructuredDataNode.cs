using System;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataNode
    {
        string Name { get; }
        IStructuredDataObject Parent { get; }
        StringComparer KeyComparer { get; }
    }
}
