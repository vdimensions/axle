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
        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator()
        {
            yield return new PropertiesValueExtractor();
            yield return new PropertiesFileExtractor();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
