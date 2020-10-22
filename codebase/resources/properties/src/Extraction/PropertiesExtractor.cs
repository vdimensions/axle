using System.Text;
using System.Threading.Tasks;
using Axle.Resources.Extraction;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a java properties file.
    /// </summary>
    public sealed class PropertiesExtractor : IResourceExtractor
    {
        private readonly IResourceExtractor _impl;

        private PropertiesExtractor(Encoding encoding, IResourceExtractor propertiesValueExtractor)
        {
            _impl = CompositeResourceExtractor.Create(
                new PropertiesFileExtractor(encoding),
                propertiesValueExtractor);
        }
        private PropertiesExtractor(Encoding encoding, string fileName, string keyPrefix = null)
            : this(encoding, new PropertiesValueExtractor(encoding, fileName, keyPrefix ?? string.Empty)) { }
        private PropertiesExtractor(Encoding encoding) 
            : this(encoding, new ImmediatePropertiesValueExtractor(encoding)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesExtractor"/> class using the specified
        /// <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">
        /// The name of the properties file containing the values to extract.
        /// </param>
        public PropertiesExtractor(string fileName) : this(Encoding.UTF8, fileName) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesExtractor"/> class.
        /// </summary>
        public PropertiesExtractor() : this(Encoding.UTF8) { }

        ResourceInfo IResourceExtractor.Extract(IResourceContext context, string name) 
            => _impl.Extract(context, name);

        Task<ResourceInfo> IResourceExtractor.ExtractAsync(IResourceContext context, string name) 
            => _impl.ExtractAsync(context, name);
    }
}
