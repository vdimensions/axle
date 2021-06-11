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
            YamlValueExtractor.DeconstructYamlFilePath(location.ToString(), out yamlFileName, out keyPrefix);

            return !string.IsNullOrEmpty(yamlFileName) && yamlFileName.EndsWith(YamlResourceInfo.FileExtension, StringComparison.OrdinalIgnoreCase);
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