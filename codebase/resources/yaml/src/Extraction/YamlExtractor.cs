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
        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator()
        {
            yield return new YamlValueExtractor();
            yield return new YamlFileExtractor();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}