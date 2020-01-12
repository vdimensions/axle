using System.Collections.Generic;

namespace Axle.Text.StructuredData.Xml
{
    internal sealed class XmlDataNode
    {
        public XmlDataNode(string name, string value, IList<XmlDataNode> children)
        {
            Name = name;
            Children = children;
            Value = (children.Count > 0 || string.IsNullOrEmpty(value)) ? null : value;
        }

        public string Name { get; }
        public string Value { get; }
        public IEnumerable<XmlDataNode> Children { get; }
    }
}