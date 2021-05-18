using System.Text;
using Axle.Resources.Extraction;

namespace Axle.Resources.Yaml.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a YAML file.
    /// </summary>
    public sealed class YamlExtractor : ResourceExtractorDecorator
    {
        private YamlExtractor(Encoding encoding, IResourceExtractor valueResourceExtractor)
            : base(CompositeResourceExtractor.Create(new YamlFileExtractor(encoding), valueResourceExtractor)) { }
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
        /// <param name="keyPrefix">
        /// An optional string to prefix each key.
        /// </param>
        public YamlExtractor(string fileName, string keyPrefix = null)  : this(Encoding.UTF8, fileName, keyPrefix) { }
    }
}