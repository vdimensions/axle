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
    public sealed class JavaPropertiesFileExtractor : IResourceExtractor
    {
        /// <summary>
        /// Creates a new instance of the <see cref="JavaPropertiesFileExtractor"/> class.
        /// </summary>
        public JavaPropertiesFileExtractor() : base() { }

        /// <inheritdoc />
        /// <summary>Attempts to locate a Java properties resource based on the provided parameters. </summary>
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            var utf8 = Encoding.UTF8;
            var finalProperties = new Dictionary<string, string>(StringComparer.Ordinal);
            var propertiesFileStream = context.ExtractionChain.Extract(name);
            using (var stream = propertiesFileStream?.Open())
            {
                if (stream == null)
                {
                    return null;
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

            return new JavaPropertiesResourceInfo(name, context.Culture, finalProperties);
        }
    }
}