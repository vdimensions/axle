using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataAdapter
    {
        string Name { get; }
        string Value { get; }
        IEnumerable<IStructuredDataAdapter> Children { get; }
    }
}