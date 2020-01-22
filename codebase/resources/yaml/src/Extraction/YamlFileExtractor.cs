using System;
using System.Collections.Generic;
using System.Globalization;

using Axle.Resources.Text.Data;
using Axle.Text.Data;
using Axle.Text.Data.Yaml;


namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlFileExtractor : AbstractTextDataExtractor
    {
        internal static StringComparer DefaultKeyComparer => StringComparer.Ordinal;

        protected override ITextDataReader GetReader(StringComparer comparer)
        {
            return new YamlDataReader(comparer);
        }

        protected override TextDataResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data)
        {
            return new YamlResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}
