using System.Globalization;
using System.Xml;
using System.Xml.Linq;


namespace Axle.Resources.Xml
{
    /// <summary>
    /// A class that represents a XML resources using <see cref="XDocument"/>.
    /// </summary>
    public sealed class XDocumentResourceInfo : XmlResourceInfo
    {
        internal XDocumentResourceInfo(string name, CultureInfo culture, XDocument value) : base(name, culture)
        {
            Value = value;
        }
        internal XDocumentResourceInfo(string name, CultureInfo culture, XDocument value, ResourceInfo originalResource) 
            : base(name, culture, originalResource)
        {
            Value = value;
        }

        /// <inheritdoc />
        protected override void WriteTo(XmlWriter writer) => Value.WriteTo(writer);

        /// <summary>
        /// Gets a reference to the <see cref="XDocument"/> instance representing the current XML resource.
        /// </summary>
        public XDocument Value { get; }
    }
}