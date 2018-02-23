#if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
using System.Globalization;
using System.Xml;


namespace Axle.Resources.Xml
{
    /// <summary>
    /// A class that represents a XML resources using <see cref="XmlDocument"/>.
    /// </summary>
    public sealed class XmlDocumentResourceInfo : XmlResourceInfo
    {
        internal XmlDocumentResourceInfo(string name, CultureInfo culture, XmlDocument value) : base(name, culture)
        {
            Value = value;
        }
        internal XmlDocumentResourceInfo(string name, CultureInfo culture, XmlDocument value, ResourceInfo originalResource) 
            : base(name, culture, originalResource)
        {
            Value = value;
        }

        /// <inheritdoc />
        protected override void WriteTo(XmlWriter writer) => Value.WriteTo(writer);

        /// <summary>
        /// Gets a reference to the <see cref="XmlDocument"/> instance representing the current XML resource.
        /// </summary>
        public XmlDocument Value { get; }
    }
}
#endif