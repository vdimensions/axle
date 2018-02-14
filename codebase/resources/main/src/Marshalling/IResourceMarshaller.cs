using System;

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
        bool TryUnmarshal(ResourceContext context, IResourceExtractor extractor, string name, Type targetType, out object result);
    }
}
