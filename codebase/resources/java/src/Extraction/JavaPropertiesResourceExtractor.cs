using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axle.Resources.Extraction;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java.Extraction
{
    public sealed class JavaPropertiesResourceExtractor : ResourceExtractorChain
    {
        private const string PropertiesMimeType = "text/x-java-properties";

        public JavaPropertiesResourceExtractor() : base() { }

        public override ResourceInfo Extract(ResourceExtractionContext context, string name, IResourceExtractor nextInChain)
        {
            var utf8 = Encoding.UTF8;
            var finalProperties = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var localContext in context.Split().Reverse())
            {
                var propertiesResource = nextInChain.Extract(localContext, name);
                var stream = propertiesResource?.Open();
                if (stream == null)
                {
                    continue;
                }
                try
                {
                    var propertiesFile = new JavaProperties();
                    propertiesFile.Load(stream, utf8);
                    foreach (var key in propertiesFile.Keys)
                    {
                        finalProperties[key] = propertiesFile[key];
                    }
                }
                catch
                {
                    continue;
                }
                finally
                {
                    stream.Dispose();
                }
            }

            return new JavaPropertiesResourceInfo(name, context.Culture, PropertiesMimeType, finalProperties);
        }
    }
}