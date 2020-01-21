using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.StructuredData.Xml
{
    internal sealed class XmlNodeAdapter : IStructuredDataAdapter
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
        IEnumerable<IStructuredDataAdapter> IStructuredDataAdapter.Children => Children.Cast<IStructuredDataAdapter>();
    }
}