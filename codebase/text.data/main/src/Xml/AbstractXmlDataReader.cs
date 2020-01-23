using System;
using System.IO;
using System.Text;

namespace Axle.Text.Data.Xml
{
    public abstract class AbstractXmlDataReader : AbstractTextDataReader
    {
        protected AbstractXmlDataReader(StringComparer comparer) : base(comparer) { }

        internal abstract XmlNodeAdapter GetXmlRoot(Stream stream, Encoding encoding);
        internal abstract XmlNodeAdapter GetXmlRoot(string xml);

        protected sealed override ITextDataAdapter CreateAdapter(Stream stream, Encoding encoding) 
            => GetXmlRoot(stream, encoding);
        protected sealed override ITextDataAdapter CreateAdapter(string data) => GetXmlRoot(data);
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