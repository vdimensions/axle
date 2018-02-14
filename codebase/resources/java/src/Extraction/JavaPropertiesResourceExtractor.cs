using System;
using System.Collections.Generic;
using System.Text;

using Axle.Resources.Extraction;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementations capable of creating Java properties files.
    /// </summary>
    public sealed class JavaPropertiesFileExtractor : ResourceExtractorChain
    {
        /// <summary>
        /// Creates a new instance of the <see cref="JavaPropertiesFileExtractor"/> class.
        /// </summary>
        public JavaPropertiesFileExtractor() : base() { }

        /// <inheritdoc />
        /// <summary>Attempts to locate a Java properties resource based on the provided parameters. </summary>
        public override ResourceInfo Extract(ResourceContext context, string name, IResourceExtractor nextInChain)
        {
            var utf8 = Encoding.UTF8;
            var finalProperties = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var localContext in context.Split(ResourceContextSplitStrategy.ByLocation))
            {
                using (var stream = nextInChain.Extract(localContext, name)?.Open())
                {
                    if (stream == null)
                    {
                        continue;
                    }
                    var propertiesFile = new JavaProperties();
                    propertiesFile.Load(stream, utf8);
                    foreach (var key in propertiesFile.Keys)
                    {
                        if (finalProperties.ContainsKey(key))
                        {
                            continue;
                        }
                        finalProperties[key] = propertiesFile[key];
                    }
                }
            }

            return new JavaPropertiesResourceInfo(name, context.Culture, finalProperties);
        }
    }
}