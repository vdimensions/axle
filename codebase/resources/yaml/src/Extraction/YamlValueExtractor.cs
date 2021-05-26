using System.Collections.Generic;
using System.Text;
using Axle.Resources.Extraction;
using Axle.Text;

namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlValueExtractor : AbstractResourceExtractor
    {
        private readonly string _yamlFile;
        private readonly string _keyPrefix;
        private readonly Encoding _encoding;

        public YamlValueExtractor(Encoding encoding, string yamlFile, string keyPrefix)
        {
            _yamlFile = yamlFile;
            _keyPrefix = keyPrefix;
            _encoding = encoding;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            IDictionary<string, CharSequence> data = new Dictionary<string, CharSequence>(YamlFileExtractor.DefaultKeyComparer);
            var yamlFile = context.Extract(_yamlFile);
            switch (yamlFile)
            {
                case null:
                    return null;
                case YamlResourceInfo yaml:
                    foreach (var kvp in yaml.Data)
                    {
                        data[kvp.Key] = kvp.Value;
                    }

                    break;
                //default:
                //    using (var stream = propertyResource.Value.Open())
                //    if (stream != null)
                //    {
                //        var x = new PropertiesFileExtractor();
                //        x.ReadData(stream, props = new Dictionary<string, string>(PropertiesFileExtractor.DefaultKeyComparer));
                //    }
                //    break;
            }

            if (data != null && data.TryGetValue($"{_keyPrefix}{name}", out var result))
            {
                // TODO: avoid calling `result.ToString()` on a CharSequence. Change resource info type appropriately
                return new TextResourceInfo(name, context.Culture, result.ToString(), _encoding);
            }

            return null;
        }
    }
}