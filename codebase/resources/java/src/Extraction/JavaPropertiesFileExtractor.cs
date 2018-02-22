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
    internal sealed class JavaPropertiesFileExtractor : IResourceExtractor
    {
        /// <inheritdoc />
        /// <summary>Attempts to locate a Java properties resource based on the provided parameters. </summary>
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            var utf8 = Encoding.UTF8;
            var finalProperties = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var propertiesFileResourceInfo in context.ExtractionChain.ExtractAll(name))
            {
                using (var stream = propertiesFileResourceInfo?.Open())
                {
                    if (stream == null)
                    {
                        continue;
                    }

                    var propertiesFile = new JavaProperties();
                    propertiesFile.Load(stream, utf8);
                    foreach (var key in propertiesFile.Keys)
                    {
                        if (!finalProperties.ContainsKey(key))
                        {
                            finalProperties[key] = propertiesFile[key];
                        }                        
                    }
                }
            }
            return finalProperties.Count == 0 ? null : new JavaPropertiesResourceInfo(name, context.Culture, finalProperties);
        }
    }
}