using System.Globalization;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a resource extractor; that is, an object responsible for locating raw resources before being unmarshalled.
    /// </summary>
    /// <seealso cref="Axle.Resources.Marshalling.IResourceMarshaller"/>
    public interface IResourceExtractor
    {
        /// <summary>
        /// Attempts to locate a raw resource based on the provided parameters.
        /// </summary>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo"/> object representing the desired culture for the resource.
        /// <remarks>
        /// The resource extractor will not attempt to locate the resource for any parent cultures of the supplied <paramref name="culture"/>, even if they would contain a matching resource.
        /// <seealso cref="CultureInfo.Parent"/>
        /// </remarks>
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResourceInfo"/> representing the extracted resource.
        /// </returns>
        ResourceInfo Extract(string name, CultureInfo culture);
    }
}