using System.IO;
using System.Text;
using System.Xml.Linq;
using Axle.Resources.Extraction;

namespace Axle.Resources.Xml.Extraction
{
    /// <summary>
    /// An implementation of  <see cref="IResourceExtractor"/> that loads XML documents using the <see cref="XDocument"/> class.
    /// </summary>
    public class XDocumentExtractor : AbstractXmlExtractor<XDocumentResourceInfo>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly LoadOptions _loadOptions;

        /// <summary>
        /// Creates a new instance of the <see cref="XDocumentExtractor"/> class.
        /// </summary>
        public XDocumentExtractor() : this(LoadOptions.None) { }
        /// <summary>
        /// Creates a new instance of the <see cref="XDocumentExtractor"/> class.
        /// </summary>
        public XDocumentExtractor(LoadOptions options) { _loadOptions = options; }

        /// <inheritdoc />
        protected override XDocumentResourceInfo ExtractXml(IResourceContext context, string name, ResourceInfo resource)
        {
            XDocument xml;
            switch (resource)
            {
                case TextResourceInfo textResource:
                    using (var tr = new StringReader(textResource.Value))
                    {
                        xml = XDocument.Load(tr, _loadOptions);
                    }
                    break;
                default:
                    using (var stream = resource.Open())
                    using (var reader = new StreamReader(stream, Encoding.UTF8, true))
                    {
                        xml = XDocument.Load(reader, _loadOptions);
                    }
                    break;
            }
            return new XDocumentResourceInfo(name, context.Culture, xml, resource);
        }
    }
}
