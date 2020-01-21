using System;
using System.IO;
using System.Text;

namespace Axle.Text.StructuredData.Xml
{
    public abstract class AbstractXmlDataReader : AbstractStructuredDataReader
    {
        protected AbstractXmlDataReader(StringComparer comparer) : base(comparer) { }

        internal abstract XmlNodeAdapter GetXmlRoot(Stream stream, Encoding encoding);
        internal abstract XmlNodeAdapter GetXmlRoot(string xml);

        protected sealed override IStructuredDataAdapter CreateAdapter(Stream stream, Encoding encoding) 
            => GetXmlRoot(stream, encoding);
        protected sealed override IStructuredDataAdapter CreateAdapter(string data) => GetXmlRoot(data);
    }

    public abstract class AbstractXmlDataReader<TXml> : AbstractXmlDataReader
    {
        protected AbstractXmlDataReader(StringComparer comparer) : base(comparer)
        {
        }
        
        protected abstract TXml GetXmlObject(Stream stream, Encoding encoding);
        protected abstract TXml GetXmlObject(string xml);

        internal abstract XmlNodeAdapter CreateXmlNode(TXml xmlObj);

        internal sealed override XmlNodeAdapter GetXmlRoot(Stream stream, Encoding encoding) => CreateXmlNode(GetXmlObject(stream, encoding));
        internal sealed override XmlNodeAdapter GetXmlRoot(string xml) => CreateXmlNode(GetXmlObject(xml));
    }
}