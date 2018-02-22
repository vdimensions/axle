using System.Collections;
using System.Collections.Generic;

using Axle.Resources.Extraction;


namespace Axle.Resources.Java.Extraction
{
    /// <summary>
    /// A class representing the resource extractor chain for the contents of a java properties file.
    /// </summary>
    public sealed class JavaPropertiesExtractor : IEnumerable<IResourceExtractor>
    {
        /// <inheritdoc />
        public IEnumerator<IResourceExtractor> GetEnumerator()
        {
            yield return new JavaPropertiesValueExtractor();
            yield return new JavaPropertiesFileExtractor();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
