using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Axle.References;
using Axle.Resources.Extraction;
using Axle.Text.Data;

namespace Axle.Resources.Text.Data
{
    public abstract class AbstractTextDataExtractor : AbstractResourceExtractor
    {
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties) => ReadData(stream, finalProperties, Encoding.UTF8);
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties, Encoding encoding)
        {
            ExtractData(
                string.Empty, 
                GetReader(KeyComparer).Read(stream, encoding), 
                finalProperties);
        }

        private static void ExtractData(string prefix, ITextDataNode structure, IDictionary<string, string> finalProperties)
        {
            var key = string.IsNullOrEmpty(prefix) ? structure.Key : $"{prefix}.{structure.Key}";
            switch (structure)
            {
                case ITextDataValue v:
                    finalProperties[key] = v.Value;
                    break;
                case ITextDataObject obj:
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

        protected abstract ITextDataReader GetReader(StringComparer comparer);
        
        protected abstract TextDataResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data);
        
        protected abstract StringComparer KeyComparer { get; }
    }
}