using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace Axle.Resources.Xml
{
    public sealed class XDocumentResourceInfo : XmlResourceInfo
    {
        internal XDocumentResourceInfo(string name, CultureInfo culture, XDocument value) : base(name, culture)
        {
            Value = value;
        }
        internal XDocumentResourceInfo(string name, CultureInfo culture, XDocument value, ResourceInfo originalResource) : base(name, culture, originalResource)
        {
            Value = value;
        }

        protected override Stream DoOpen()
        {
            var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { CloseOutput = false }))
            {
                Value.WriteTo(writer);
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public XDocument Value { get; }
    }
}