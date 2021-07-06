#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Axle.Text.Documents.Xml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XDocumentReader : AbstractXmlTextDocumentReader<XDocument>
    {
        public XDocumentReader(StringComparer comparer) : base(comparer) { }
        
        protected override XDocument GetXmlObject(Stream stream, Encoding encoding) 
            => XDocument.Load(new StreamReader(stream, encoding));
        protected override XDocument GetXmlObject(string xml) 
            => XDocument.Parse(xml);

        internal override XmlDocumentAdapter CreateXmlNode(XDocument xmlObj)
        {
            return new XmlDocumentAdapter(
                string.Empty, 
                xmlObj.Root.Value, 
                xmlObj.Root.Elements().Select(CreateXmlNode).ToArray());
        }
        private XmlDocumentAdapter CreateXmlNode(XElement xElement)
        {
            return new XmlDocumentAdapter(
                xElement.Name.LocalName, 
                xElement.Value, 
                xElement.Elements().Select(CreateXmlNode).ToArray());
        }
    }
}
#endif
