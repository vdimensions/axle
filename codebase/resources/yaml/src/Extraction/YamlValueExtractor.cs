using System.Collections.Generic;
using System.Text;
using Axle.Resources.Extraction;


namespace Axle.Resources.Yaml.Extraction
{
    public sealed class YamlValueExtractor : AbstractResourceExtractor
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
            IDictionary<string, string> data;
            switch (context.Extract(_yamlFile))
            {
                case null:
                    return null;
                case YamlResourceInfo yaml:
                    data = yaml.Data;
                    break;
                default:
                    //using (var stream = yamlRes.Value.Open())
                    //if (stream != null)
                    //{
                    //    var p = new Dictionary<string, string>();
                    //    YamlFileExtractor.ReadData(stream, p);
                    //    data = p;
                    //}
                    //else
                    //{
                        data = new Dictionary<string, string>(YamlFileExtractor.DefaultKeyComparer);
                    //}
                    break;
            }

            if (data != null && data.TryGetValue($"{_keyPrefix}{name}", out var result))
            {
                return new TextResourceInfo(name, context.Culture, result, _encoding);
            }

            return null;
        }
    }
}