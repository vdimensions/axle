using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    public abstract class AbstractStructuredDataAdapter : IStructuredDataAdapter
    {
        public abstract IDictionary<string, IStructuredDataAdapter[]> GetChildren();
        public abstract string Value { get; }
    }
}