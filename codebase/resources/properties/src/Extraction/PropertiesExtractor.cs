using System.Collections;
using System.Collections.Generic;
using Axle.Resources.Extraction;

namespace Axle.Resources.Properties.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a java properties file.
    /// </summary>
    public sealed class PropertiesExtractor : IEnumerable<IResourceExtractor>
    {
        private readonly IResourceExtractor _propertiesValueExtractor;

        private PropertiesExtractor(IResourceExtractor propertiesValueExtractor)
        {
            _propertiesValueExtractor = propertiesValueExtractor;
        }

        public PropertiesExtractor(string propertiesFileName, string keyPrefix = null)
            : this(new PropertiesValueExtractor(propertiesFileName, keyPrefix ?? string.Empty)) { }
        public PropertiesExtractor() : this(new ImmediatePropertiesValueExtractor()) { }
        
        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator()
        {
            yield return new PropertiesFileExtractor();
            yield return _propertiesValueExtractor;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
