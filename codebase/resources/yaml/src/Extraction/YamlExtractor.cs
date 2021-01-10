using System.Text;
#if !UNITY_WEBGL
using System.Threading.Tasks;
#endif
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
        
        /// <summary>
        /// Initializes a new instance of the <see cref="YamlExtractor"/> class.
        /// </summary>
        public YamlExtractor()  : this(Encoding.UTF8) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="YamlExtractor"/> class using the specified
        /// <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">
        /// The name of the yaml file containing the values to extract.
        /// </param>
        public YamlExtractor(string fileName)  : this(Encoding.UTF8, fileName) { }

        /// <inheritdoc />
        public ResourceInfo Extract(IResourceContext context, string name) 
            => _impl.Extract(context, name);

        #if !UNITY_WEBGL
        /// <inheritdoc />
        public Task<ResourceInfo> ExtractAsync(IResourceContext context, string name) 
            => _impl.ExtractAsync(context, name);
        #endif
    }
}