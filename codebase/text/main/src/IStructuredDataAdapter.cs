using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    public interface IStructuredDataAdapter
    {
        IDictionary<string, IStructuredDataAdapter[]> GetChildren();
        string Value { get; }
    }
}