using System;

using Axle.Extensions.String;
using Axle.Resources.Extraction;


namespace Axle.Resources.Xml.Extraction
{
    /// <summary>
    /// An abstract class to serve as a base for creating XML resource marshaller implementations.
    /// <para>
    /// Known implementations of this class are the  <see cref="XDocumentExtractor" /> and <see cref="XmlDocumentExtractor"/>
    /// </para>
    /// </summary>
    /// <typeparam name="TXml">
    /// The type representing the XML document from the unmarshalled resource.
    /// </typeparam>
    /// <seealso cref="XDocumentExtractor"/>
    /// <seealso cref="XmlDocumentExtractor"/>
    public abstract class AbstractXmlExtractor<TXml> : IResourceExtractor where TXml: XmlResourceInfo
    {
        protected abstract TXml Extract(ResourceContext context, string name, ResourceInfo xml);

        public ResourceInfo Extract(ResourceContext context, string name)
        {
            var xmlName = $"{name.TrimEnd(XmlResourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase)}{XmlResourceInfo.FileExtension}";
            var xmlResource = context.ExtractionChain.Extract(xmlName);
            try
            {
                return xmlResource != null ? Extract(context, name, xmlResource) : null;
            }
            catch (Exception e)
            {
                throw new ResourceLoadException(name, context.Bundle, context.Culture, e);
            }
        }
    }
}
