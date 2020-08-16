using System;
using System.Text;
using Axle.Extensions.String;
using Axle.Resources.Extraction;

namespace Axle.Resources.Yaml.Extraction
{
    internal sealed class ImmediateYamlValueExtractor : AbstractResourceExtractor
    {
        private static bool GetYamlFileData(Uri location, out string yamlFileName, out string keyPrefix)
        {
            yamlFileName = keyPrefix = null;
            const string ext = YamlResourceInfo.FileExtension;
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
            var locStr = location.ToString();
            keyPrefix = locStr.TakeAfterFirst(ext, cmp);
            yamlFileName = locStr.TakeBeforeFirst(keyPrefix, cmp);
            const char slash = '/';
            keyPrefix = keyPrefix.TrimStart(slash);
            if (keyPrefix.EndsWith(slash.ToString()))
            {
                keyPrefix = $"{keyPrefix.TrimEnd(slash)}.";
            }

            return !string.IsNullOrEmpty(yamlFileName) && yamlFileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase);
        }

        private readonly Encoding _encoding;

        public ImmediateYamlValueExtractor(Encoding encoding)
        {
            _encoding = encoding;
        }

        protected override ResourceInfo DoExtract(IResourceContext context, string name)
        {
            if (!GetYamlFileData(context.Location, out var fileName, out var keyPrefix))
            {
                return null;
            }
            return new YamlValueExtractor(_encoding, fileName, keyPrefix).Extract(context, name);
        }
    }
}