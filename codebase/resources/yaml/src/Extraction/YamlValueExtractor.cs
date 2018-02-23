using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Extensions.String;
using Axle.Resources.Extraction;


namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class YamlValueExtractor : IResourceExtractor
    {
        private static bool GetYamlFileData(Uri location, out string yamlFileName, out string keyPrefix)
        {
            yamlFileName = keyPrefix = null;
            const string ext = YamlResourceInfo.FileExtension;
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
            var locStr = location.ToString();
            keyPrefix = locStr.TakeAfterFirst(ext, cmp);
            yamlFileName = locStr.TakeBeforeFirst(keyPrefix, cmp);

            return !string.IsNullOrEmpty(yamlFileName) && yamlFileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);
        }

        public ResourceInfo Extract(ResourceContext context, string name)
        {
            if (!GetYamlFileData(context.Location, out var fileName, out var keyPrefix))
            {
                return null;
            }

            IEnumerable<IDictionary<string, string>> data;
            var yamlStream = context.ExtractionChain.Extract(fileName);
            switch (yamlStream)
            {
                case YamlResourceInfo yaml:
                    data = yaml.Data;
                    break;
                default:
                    using (var stream = yamlStream?.Open())
                        if (stream != null)
                        {
                            var p = new List<IDictionary<string, string>>();
                            YamlFileExtractor.ReadData(stream, p);
                            data = p;
                        }
                        else
                        {
                            data = Enumerable.Empty<IDictionary<string, string>>();
                        }
                    break;
            }

            foreach (var props in data)
            {                    
                if (props != null && props.TryGetValue($"{keyPrefix}{name}", out var result))
                {
                    return new TextResourceInfo(name, context.Culture, result);
                }
            }

            return null;
        }
    }
}