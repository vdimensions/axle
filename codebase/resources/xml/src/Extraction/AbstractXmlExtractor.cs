using System;

using Axle.Resources.Extraction;


namespace Axle.Resources.Xml.Extraction
{
    #if !NETSTANDARD || NETSTANDARD1_6_OR_NEWER
    /// <summary>
    /// An abstract class to serve as a base for creating XML resource marshaller implementations.
    /// <para>
    /// Known implementations of this class are the <see cref="XDocumentExtractor" /> and <see cref="XmlDocumentExtractor"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="TX">
    /// The type representing the XML document from the unmarshalled resource.
    /// </typeparam>
    /// <seealso cref="XDocumentExtractor"/>
    /// <seealso cref="XmlDocumentExtractor"/>
    #else
    /// <summary>
    /// An abstract class to serve as a base for creating XML resource marshaller implementations.
    /// <para>
    /// Known implementations of this class is the <see cref="XDocumentExtractor" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TX">
    /// The type representing the XML document from the unmarshalled resource.
    /// </typeparam>
    /// <seealso cref="XDocumentExtractor"/>
    #endif
    public abstract class AbstractXmlExtractor<TX> : AbstractResourceExtractor where TX: XmlResourceInfo
    {
        /// <summary>
        /// Extracts a <typeparamref name="TX"/> representation of an XML resource.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ResourceContext"/> used for the extraction. 
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource. 
        /// </param>
        /// <param name="resource">
        /// The original <see cref="ResourceInfo"/> object that will be used to stream the underlying XML.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TX"/> representing the XML resource. 
        /// </returns>
        protected abstract TX ExtractXml(ResourceContext context, string name, ResourceInfo resource);

        /// <inheritdoc />
        protected sealed override ResourceInfo DoExtract(ResourceContext context, string name)
        {
            var xmlResource = context.ExtractionChain.Extract(name);
            try
            {
                return xmlResource != null ? ExtractXml(context, name, xmlResource) : null;
            }
            catch (Exception e)
            {
                throw new ResourceLoadException(name, context.Bundle, context.Culture, e);
            }
        }
    }
}
