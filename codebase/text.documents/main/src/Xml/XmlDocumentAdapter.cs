using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.Documents.Xml
{
    internal sealed class XmlDocumentAdapter : ITextDocumentAdapter
    {
        public XmlDocumentAdapter(string name, string value, ICollection<XmlDocumentAdapter> children)
        {
            Key = name;
            Children = children;
            Value = (children.Count > 0 || string.IsNullOrEmpty(value)) ? null : value;
        }

        public string Key { get; }
        public string Value { get; }
        public IEnumerable<XmlDocumentAdapter> Children { get; }
        IEnumerable<ITextDocumentAdapter> ITextDocumentAdapter.Children => Children.Cast<ITextDocumentAdapter>();
    }
}