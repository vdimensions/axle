using System.Threading.Tasks;

using Axle.References;
using Axle.Verification;


namespace Axle.Resources.Extraction
{
    /// <inheritdoc />
    public abstract class AbstractResourceExtractor : IResourceExtractor
    {
        /// <inheritdoc />
        public Nullsafe<ResourceInfo> Extract(ResourceContext context, string name)
        {
            return DoExtract(context.VerifyArgument(nameof(context)).IsNotNull(), name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
        }

        /// <inheritdoc />
        #if NETSTANDARD || NET45_OR_NEWER
        public virtual async Task<Nullsafe<ResourceInfo>> ExtractAsync(ResourceContext context, string name)
        #elif NET35_OR_NEWER
        public virtual Task<Nullsafe<ResourceInfo>> ExtractAsync(ResourceContext context, string name)
        #endif
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            #if NETSTANDARD || NET45_OR_NEWER
            return await Task.Run(() => DoExtract(context, name));
            #elif NET40
            return Task.Factory.StartNew(() => DoExtract(context, name));
            #else
            return Task.Run(() => DoExtract(context, name));
            #endif
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
        protected virtual Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name) => context.ExtractionChain.Extract(name);
    }
}