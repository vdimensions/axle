using System.Collections.Generic;

namespace Axle.Text.Data
{
    public interface ITextDataAdapter
    {
        string Key { get; }
        string Value { get; }
        IEnumerable<ITextDataAdapter> Children { get; }
    }
}