using System;

namespace Axle.Text.Data
{
    public interface ITextDataNode
    {
        ITextDataObject Parent { get; }
        string Key { get; }
        StringComparer KeyComparer { get; }
    }
}
