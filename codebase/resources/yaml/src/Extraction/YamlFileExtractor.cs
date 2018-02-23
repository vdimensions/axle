using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Axle.Resources.Extraction;

using YamlDotNet.Serialization;


namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlFileExtractor : IResourceExtractor
    {
        internal static IEqualityComparer<string> KeyComparer => StringComparer.Ordinal;

        internal static void ReadData(Stream stream, ICollection<IDictionary<string, string>> finalProperties) => ReadData(stream, finalProperties, Encoding.UTF8);
        private static void ReadData(Stream stream, ICollection<IDictionary<string, string>> finalProperties, Encoding encoding)
        {
            var result = ReadYaml(stream, encoding);

            foreach (var table in result)
            {
                finalProperties.Add(table);
            }
        }

        internal static IEnumerable<IDictionary<string, string>> ReadYaml(Stream stream, Encoding encoding)
        {
            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<List<Dictionary<string, string>>>(new StreamReader(stream, encoding, true));
            return result.Cast<IDictionary<string, string>>();
        }

        public ResourceInfo Extract(ResourceContext context, string name)
        {
            var finalProperties = new List<IDictionary<string, string>>();
            foreach (var propertiesFileResourceInfo in context.ExtractionChain.ExtractAll(name))
            {
                using (var stream = propertiesFileResourceInfo?.Open())
                {
                    if (stream == null)
                    {
                        continue;
                    }
                    ReadData(stream, finalProperties);
                }
            }
            return finalProperties.Count == 0 ? null : new YamlResourceInfo(name, context.Culture, finalProperties);
        }
    }
}
