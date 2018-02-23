using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

using Axle.Resources.Extraction;


namespace Axle.Resources.Xml.Extraction
{
    public class XDocumentExtractor : AbstractXmlExtractor<XDocumentResourceInfo>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly LoadOptions _loadOptions;

        public XDocumentExtractor() : this(LoadOptions.None) { }
        public XDocumentExtractor(LoadOptions options) { _loadOptions = options; }

        protected override XDocumentResourceInfo Extract(ResourceContext context, string name, ResourceInfo resource)
        {
            XDocument xml = null;
            switch (resource)
            {
                case TextResourceInfo textResource:
                    using (var tr = new StringReader(textResource.Value))
                    {
                        xml = XDocument.Load(tr);
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
