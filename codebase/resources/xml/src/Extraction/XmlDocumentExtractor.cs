#if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
using System.Xml;

using Axle.Resources.Extraction;


namespace Axle.Resources.Xml.Extraction
{
    /// <summary>
    /// An implementation of <see cref="IResourceExtractor"/> that loads XML documents using the <see cref="XmlDocument"/> class.
    /// </summary>
    public class XmlDocumentExtractor : AbstractXmlExtractor<XmlDocumentResourceInfo>
    {
        /// <inheritdoc />
        protected override XmlDocumentResourceInfo Extract(ResourceContext context, string name, ResourceInfo resource)
        {
            var xml = new XmlDocument();
            switch (resource)
            {
                case TextResourceInfo textResource:
                    xml.LoadXml(textResource.Value);
                    break;
                default:
                    using (var xmlStream = resource.Open())
                    {
                        xml.Load(xmlStream);
                    }
                    break;
            }
            return new XmlDocumentResourceInfo(name, context.Culture, xml, resource);
        }
    }
}
#endif