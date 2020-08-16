using System.Text;
using System.Threading.Tasks;
using Axle.Resources.Extraction;

namespace Axle.Resources.Yaml.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a YAML file.
    /// </summary>
    public sealed class YamlExtractor : IResourceExtractor
    {
        private readonly IResourceExtractor _impl;

        private YamlExtractor(Encoding encoding, IResourceExtractor valueResourceExtractor)
        {
            _impl = CompositeResourceExtractor.Create(
                new YamlFileExtractor(encoding),
                valueResourceExtractor);
        }
        private YamlExtractor(Encoding encoding) 
            : this(encoding, new ImmediateYamlValueExtractor(encoding)) { }
        private YamlExtractor(Encoding encoding, string fileName, string keyPrefix = null) 
            : this(encoding, new YamlValueExtractor(encoding, fileName, keyPrefix ?? string.Empty)) { }
        
        public YamlExtractor()  : this(Encoding.UTF8) { }
        public YamlExtractor(string fileName, string keyPrefix = null)  : this(Encoding.UTF8, fileName, keyPrefix) { }

        public ResourceInfo Extract(IResourceContext context, string name) 
            => _impl.Extract(context, name);

        public Task<ResourceInfo> ExtractAsync(IResourceContext context, string name) 
            => _impl.ExtractAsync(context, name);
    }
}