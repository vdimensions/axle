using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Data.Xml
{
    internal sealed class XmlNodeAdapter : ITextDataAdapter
    {
        public XmlNodeAdapter(string name, string value, ICollection<XmlNodeAdapter> children)
        {
            Key = name;
            Children = children;
            Value = (children.Count > 0 || string.IsNullOrEmpty(value)) ? null : value;
        }

        public string Key { get; }
        public string Value { get; }
        public IEnumerable<XmlNodeAdapter> Children { get; }
        IEnumerable<ITextDataAdapter> ITextDataAdapter.Children => Children.Cast<ITextDataAdapter>();
    }
}