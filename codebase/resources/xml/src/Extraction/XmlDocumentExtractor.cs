using System.Xml;

using Axle.Resources.Extraction;


namespace Axle.Resources.Xml.Extraction
{
    public class XmlDocumentExtractor : AbstractXmlExtractor<XmlDocumentResourceInfo>
    {
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
