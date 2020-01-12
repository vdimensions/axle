using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Axle.Text.StructuredData.Xml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XDocumentDataReader : AbstractXmlDataReader<XDocument>
    {
        public XDocumentDataReader(StringComparer comparer) : base(comparer) { }
        
        protected override XDocument GetXmlObject(Stream stream, Encoding encoding) 
            => XDocument.Load(new StreamReader(stream, encoding));
        protected override XDocument GetXmlObject(string xml) 
            => XDocument.Parse(xml);

        internal override XmlDataNode CreateXmlNode(XDocument xmlObj)
        {
            return new XmlDataNode(
                string.Empty, 
                xmlObj.Root.Value, 
                xmlObj.Root.Elements().Select(CreateXmlNode).ToArray());
        }
        private XmlDataNode CreateXmlNode(XElement xElement)
        {
            return new XmlDataNode(
                xElement.Name.LocalName, 
                xElement.Value, 
                xElement.Elements().Select(CreateXmlNode).ToArray());
        }
    }
}
