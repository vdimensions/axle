using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Axle.Resources.Text.Documents;
using Axle.Text;
using Axle.Text.Documents;
using Axle.Text.Documents.Yaml;

namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlFileExtractor : AbstractTextDocumentExtractor
    {
        internal static StringComparer DefaultKeyComparer => StringComparer.Ordinal;

        protected override ITextDocumentReader GetReader(StringComparer comparer) => new YamlDocumentReader(comparer);

        protected override TextDocumentResourceInfo CreateResourceInfo(
                string name, 
                CultureInfo culture, 
                IDictionary<string, CharSequence> data) =>
            new YamlResourceInfo(name, culture, data);

        public YamlFileExtractor(Encoding encoding) : base(encoding) { }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}
