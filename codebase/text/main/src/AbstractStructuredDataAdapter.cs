using System.Collections.Generic;

namespace Axle.Text.StructuredData
{
    public abstract class AbstractStructuredDataAdapter : IStructuredDataAdapter
    {
        public abstract string Name { get; }
        public abstract string Value { get; }
        public abstract IEnumerable<IStructuredDataAdapter> Children { get; }
    }
}