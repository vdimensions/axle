using System.Globalization;
using System.IO;
using System.Xml;


namespace Axle.Resources.Xml
{
    public sealed class XmlDocumentResourceInfo : XmlResourceInfo
    {
        internal XmlDocumentResourceInfo(string name, CultureInfo culture, XmlDocument value) : base(name, culture)
        {
            Value = value;
        }
        internal XmlDocumentResourceInfo(string name, CultureInfo culture, XmlDocument value, ResourceInfo originalResource) : base(name, culture, originalResource)
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

        public XmlDocument Value { get; }
    }
}