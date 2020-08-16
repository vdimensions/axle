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

        public PropertiesExtractor(string fileName, string keyPrefix = null) : this(Encoding.UTF8, fileName, keyPrefix) { }
        public PropertiesExtractor() : this(Encoding.UTF8) { }

        ResourceInfo IResourceExtractor.Extract(IResourceContext context, string name) 
            => _impl.Extract(context, name);

        Task<ResourceInfo> IResourceExtractor.ExtractAsync(IResourceContext context, string name) 
            => _impl.ExtractAsync(context, name);
    }
}
