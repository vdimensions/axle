using System;
using System.Globalization;

using Axle.Resources.Extraction;


namespace Axle.Resources.Marshalling
{
    /// <summary>
    /// An interface representing a resource marshaller; that is, an object which converts a raw resource data to a meaningful programatic construct.
    /// </summary>
    /// <seealso cref="IResourceExtractor" />
    public interface IResourceMarshaller
    {
        /// <summary>
        /// Attempts to unmarshall a resource.
        /// </summary>
        bool TryUnmarshal(IResourceExtractor extractor, string name, CultureInfo culture, Type targetType, out object result);
    }
}
