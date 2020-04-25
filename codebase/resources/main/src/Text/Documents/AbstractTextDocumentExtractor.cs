using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Axle.Resources.Extraction;
using Axle.Text.Documents;

namespace Axle.Resources.Text.Documents
{
    public abstract class AbstractTextDocumentExtractor : AbstractResourceExtractor
    {
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties) => ReadData(stream, finalProperties, Encoding.UTF8);
        private void ReadData(Stream stream, IDictionary<string, string> finalProperties, Encoding encoding)
        {
            ExtractData(
                string.Empty, 
                GetReader(KeyComparer).Read(stream, encoding), 
                finalProperties);
        }

        private static void ExtractData(string prefix, ITextDocumentNode structure, IDictionary<string, string> finalProperties)
        {
            var key = string.IsNullOrEmpty(prefix) ? structure.Key : $"{prefix}.{structure.Key}";
            switch (structure)
            {
                case ITextDocumentValue v:
                    finalProperties[key] = v.Value;
                    break;
                case ITextDocumentObject obj:
                    foreach (var child in obj.GetChildren())
                    {
                        ExtractData(key, child, finalProperties);
                    }
                    break;
            }
        }

        /// <inheritdoc />
        /// <summary>Attempts to locate a Java properties resource based on the provided parameters. </summary>
        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            var finalProperties = new Dictionary<string, string>(KeyComparer);
            foreach (var propertiesFileResourceInfo in context.ExtractAll(name))
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

        protected abstract ITextDocumentReader GetReader(StringComparer comparer);
        
        protected abstract TextDocumentResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data);
        
        protected abstract StringComparer KeyComparer { get; }
    }
}