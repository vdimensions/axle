using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Axle.Text.StructuredData.Xml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlDataReader : AbstractXmlDataReader<XmlDocument>
    {
        public XmlDataReader(StringComparer comparer) : base(comparer) { }

        protected override XmlDocument GetXmlObject(Stream stream, Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.Load(new StreamReader(stream, encoding));
            return doc;
        }

        protected override XmlDocument GetXmlObject(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        internal override XmlDataNode CreateXmlNode(XmlDocument xmlObj)
        {
            return new XmlDataNode(
                string.Empty, 
                xmlObj.Value, 
                xmlObj.ChildNodes.OfType<XmlNode>().Select(CreateXmlNode).ToArray());
        }
        private XmlDataNode CreateXmlNode(XmlNode xmlElement)
        {
            return new XmlDataNode(
                xmlElement.Name, 
                xmlElement.Value, 
                xmlElement.ChildNodes.OfType<XmlNode>().Select(CreateXmlNode).ToArray());
        }
    }
}