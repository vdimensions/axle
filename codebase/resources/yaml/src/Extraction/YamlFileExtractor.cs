using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Axle.References;
using Axle.Resources.Extraction;
using Axle.Resources.StructuredData;
using Axle.Text.StructuredData;
using Axle.Text.StructuredData.Yaml;


namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlFileExtractor : AbstractStructuredDataExtractor
    {
        internal static StringComparer DefaultKeyComparer => StringComparer.Ordinal;

        protected override IStructuredDataReader GetReader(StringComparer comparer)
        {
            return new YamlDataReader(comparer);
        }

        protected override StructuredDataResourceInfo CreateResourceInfo(string name, CultureInfo culture, IDictionary<string, string> data)
        {
            return new YamlResourceInfo(name, culture, data);
        }

        protected override StringComparer KeyComparer => DefaultKeyComparer;
    }
}
