using System;
using System.IO;
using System.Text;

namespace Axle.Text.Documents.Xml
{
    #if NETSTANDARD || NET35_OR_NEWER
    /// <summary>
    /// An abstract class serving as a base for implementing the <see cref="AbstractTextDocumentReader"/> with
    /// support for the XML format.
    /// </summary>
    /// <seealso cref="XDocumentReader"/>
    /// <seealso cref="XmlTextDocumentReader"/>
    #else
    /// <summary>
    /// An abstract class serving as a base for implementing the <see cref="AbstractTextDocumentReader"/> with
    /// support for the XML format.
    /// </summary>
    /// <seealso cref="XmlTextDocumentReader"/>
    #endif
    public abstract class AbstractXmlTextDocumentReader : AbstractTextDocumentReader
    {
        /// <summary>
        /// When called from a derived class, initializes the new <see cref="AbstractXmlTextDocumentReader"/>
        /// implementation with the provided string <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">
        /// A <see cref="StringComparer"/> instance used for comparison of the xml node keys when they are interpreted
        /// as text data nodes. 
        /// </param>
        protected AbstractXmlTextDocumentReader(StringComparer comparer) : base(comparer) { }

        internal abstract XmlDocumentAdapter GetXmlRoot(Stream stream, Encoding encoding);
        internal abstract XmlDocumentAdapter GetXmlRoot(string xml);

        protected sealed override ITextDocumentAdapter CreateAdapter(Stream stream, Encoding encoding) 
            => GetXmlRoot(stream, encoding);
        protected sealed override ITextDocumentAdapter CreateAdapter(string data) => GetXmlRoot(data);
    }

    public abstract class AbstractXmlTextDocumentReader<TXml> : AbstractXmlTextDocumentReader
    {
        protected AbstractXmlTextDocumentReader(StringComparer comparer) : base(comparer)
        {
        }
        
        protected abstract TXml GetXmlObject(Stream stream, Encoding encoding);
        protected abstract TXml GetXmlObject(string xml);

        internal abstract XmlDocumentAdapter CreateXmlNode(TXml xmlObj);

        internal sealed override XmlDocumentAdapter GetXmlRoot(Stream stream, Encoding encoding) => CreateXmlNode(GetXmlObject(stream, encoding));
        internal sealed override XmlDocumentAdapter GetXmlRoot(string xml) => CreateXmlNode(GetXmlObject(xml));
    }
}