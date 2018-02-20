using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <inheritdoc />
    public abstract class AbstractResourceExtractor : IResourceExtractor
    {
        /// <inheritdoc />
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            return DoExtract(context.VerifyArgument(nameof(context)).IsNotNull(), name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
        }

        /// <summary>
        /// Override this method to implement the actual resource extraction logic for the current <see cref="AbstractResourceExtractor"/> implementation.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceContext"/> instance that represents the context of the current resource extraction. 
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource. 
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the extracted resource, or <c>null</c> if the resource was not found. 
        /// </returns>
        protected virtual ResourceInfo DoExtract(ResourceContext context, string name) => context.ExtractionChain.Extract(name);
    }
}