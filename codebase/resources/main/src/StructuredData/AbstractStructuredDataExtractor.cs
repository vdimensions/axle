using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Axle.References;
using Axle.Resources.Extraction;
using Axle.Text.StructuredData;

namespace Axle.Resources.StructuredData
{
    public abstract class AbstractStructuredDataExtractor : AbstractResourceExtractor
    {
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties) => ReadData(stream, finalProperties, Encoding.UTF8);
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties, Encoding encoding)
        {
            ExtractData(
                string.Empty, 
                GetReader(KeyComparer).Read(stream, encoding), 
                finalProperties);
        }

        private static void ExtractData(string prefix, IStructuredDataNode structure, IDictionary<string, string> finalProperties)
        {
            var key = string.IsNullOrEmpty(prefix) ? structure.Key : $"{prefix}.{structure.Key}";
            switch (structure)
            {
                case IStructuredDataValue v:
                    finalProperties[key] = v.Value;
                    break;
                case IStructuredDataObject obj:
                    foreach (var child in obj.GetChildren())
                    {
                        ExtractData(key, child, finalProperties);
                    }
                    break;
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
            return finalProperties.Count == 0 ? null : CreateResourceInfo(name, context.Culture, finalProperties);
        }

        protected abstract IStructuredDataReader GetReader(StringComparer comparer);
        
        protected abstract StructuredDataResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data);
        
        protected abstract StringComparer KeyComparer { get; }
    }
}