using System.Globalization;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a <see cref="IResourceExtractor"/> that is part of an extraction chain.
    /// A chained extractor can use the preceding extractors to obtain other resource objects as part of its own 
    /// extraction implementation.
    /// </summary>
    public interface IResourceExtractorChain : IResourceExtractor
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
        /// <param name="nextInChain">
        /// A reference to the preceding resource extraction chain. This allows calling its
        /// own <see cref="IResourceExtractor.Extract"/> method as part of the extraction logic
        /// within the current <see cref="IResourceExtractorChain"/> implementation.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResourceInfo"/> representing the extracted resource.
        /// </returns>
        ResourceInfo Extract(string name, CultureInfo culture, IResourceExtractor nextInChain);
    }
}