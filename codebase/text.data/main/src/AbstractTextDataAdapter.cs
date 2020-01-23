using System.Collections.Generic;

namespace Axle.Text.Data
{
    public abstract class AbstractTextDataAdapter : ITextDataAdapter
    {
        public abstract string Key { get; }
        public abstract string Value { get; }
        public abstract IEnumerable<ITextDataAdapter> Children { get; }
    }
}