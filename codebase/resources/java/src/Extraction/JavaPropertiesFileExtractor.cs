using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Axle.References;
using Axle.Resources.Extraction;
using Axle.Text.StructuredData;
using Axle.Text.StructuredData.Properties;

namespace Axle.Resources.Java.Extraction
{
    /// <summary>
    /// A <see cref="IResourceExtractor"/> implementations capable of creating Java properties files.
    /// </summary>
    internal sealed class JavaPropertiesFileExtractor : AbstractResourceExtractor
    {
        internal static IEqualityComparer<string> KeyComparer => StringComparer.Ordinal;

        internal static void ReadData(Stream stream, IDictionary<string, string> finalProperties) => ReadData(stream, finalProperties, Encoding.UTF8);
        private static void ReadData(Stream stream, IDictionary<string, string> finalProperties, Encoding encoding)
        {
            var structure = new PropertiesDataReader(StringComparer.OrdinalIgnoreCase).Read(stream, encoding);
            foreach (var item in structure.GetChildren())
            {
                if (item is IStructuredDataValue v && !finalProperties.ContainsKey(v.Name))
                {
                    finalProperties[v.Name] = v.Value;
                }
            }
        }

        /// <inheritdoc />
        /// <summary>Attempts to locate a Java properties resource based on the provided parameters. </summary>
        protected override Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name)
        {
            var finalProperties = new Dictionary<string, string>(KeyComparer);
            foreach (var propertiesFileResourceInfo in context.ExtractionChain.ExtractAll(name).Where(x => x.HasValue).Select(x => x.Value))
            {
                using (var stream = propertiesFileResourceInfo.Open())
                {
                    if (stream == null)
                    {
                        continue;
                    }

                    ReadData(stream, finalProperties);
                }
            }
            return finalProperties.Count == 0 ? null : new JavaPropertiesResourceInfo(name, context.Culture, finalProperties);
        }
    }
}