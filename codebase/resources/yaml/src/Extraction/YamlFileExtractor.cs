using System;
using System.Collections.Generic;
using System.Globalization;
using Axle.Resources.Text.Documents;
using Axle.Text.Documents;
using Axle.Text.Documents.Yaml;


namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlFileExtractor : AbstractTextDocumentExtractor
    {
        internal static StringComparer DefaultKeyComparer => StringComparer.Ordinal;

        protected override ITextDocumentReader GetReader(StringComparer comparer)
        {
            return new YamlDocumentReader(comparer);
        }

        protected override TextDocumentResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data)
        {
            return new YamlResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}
