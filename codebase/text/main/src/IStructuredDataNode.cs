using System;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataNode
    {
        string Key { get; }
        IStructuredDataObject Parent { get; }
        StringComparer KeyComparer { get; }
    }
}
