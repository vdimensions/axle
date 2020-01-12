using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Axle.Text.StructuredData.Xml
{
    public abstract class AbstractXmlDataReader : AbstractStructuredDataReader
    {
        private sealed class XmlAdapter : AbstractStructuredDataAdapter 
        {
            private readonly StringComparer _comparer;
            private readonly XmlDataNode _root;
            private readonly IDictionary<string, IStructuredDataAdapter[]> _children;

            public XmlAdapter(XmlDataNode root, StringComparer comparer)
            {
                _root = root;
                _comparer = comparer;
                _children = _root.Children
                    .GroupBy(x => x.Name, comparer)
                    .ToDictionary(x => x.Key, x => x.Select(y => new XmlAdapter(y, comparer) as IStructuredDataAdapter).ToArray(), comparer);
            }


            public override IDictionary<string, IStructuredDataAdapter[]> GetChildren() => _children;
            public override string Value => _root.Value;
        }

        protected AbstractXmlDataReader(StringComparer comparer) : base(comparer) { }

        internal abstract XmlDataNode GetXmlRoot(Stream stream, Encoding encoding);
        internal abstract XmlDataNode GetXmlRoot(string xml);

        protected sealed override IStructuredDataAdapter CreateAdapter(Stream stream, Encoding encoding) 
            => new XmlAdapter(GetXmlRoot(stream, encoding), Comparer);
        protected sealed override IStructuredDataAdapter CreateAdapter(string data) 
            => new XmlAdapter(GetXmlRoot(data), Comparer);
    }

    public abstract class AbstractXmlDataReader<TXml> : AbstractXmlDataReader
    {
        protected AbstractXmlDataReader(StringComparer comparer) : base(comparer)
        {
        }
        
        protected abstract TXml GetXmlObject(Stream stream, Encoding encoding);
        protected abstract TXml GetXmlObject(string xml);

        internal abstract XmlDataNode CreateXmlNode(TXml xmlObj);

        internal sealed override XmlDataNode GetXmlRoot(Stream stream, Encoding encoding) => CreateXmlNode(GetXmlObject(stream, encoding));
        internal sealed override XmlDataNode GetXmlRoot(string xml) => CreateXmlNode(GetXmlObject(xml));
    }
}