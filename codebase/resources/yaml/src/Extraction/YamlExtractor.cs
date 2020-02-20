using System.Collections;
using System.Collections.Generic;

using Axle.Resources.Extraction;


namespace Axle.Resources.Yaml.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a YAML file.
    /// </summary>
    public sealed class YamlExtractor : IEnumerable<IResourceExtractor>
    {
        private readonly IResourceExtractor _valueResourceExtractor;

        private YamlExtractor(IResourceExtractor valueResourceExtractor)
        {
            _valueResourceExtractor = valueResourceExtractor;
        }
        public YamlExtractor() 
            : this(new ImmediateYamlValueExtractor()) { }
        public YamlExtractor(string yamlFile, string keyPrefix = null) 
            : this(new YamlValueExtractor(yamlFile, keyPrefix ?? string.Empty)) { }

        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator()
        {
            yield return new YamlFileExtractor();
            yield return _valueResourceExtractor;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}