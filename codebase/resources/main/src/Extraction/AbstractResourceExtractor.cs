using System;
using System.Threading.Tasks;
using Axle.Verification;

namespace Axle.Resources.Extraction
{
    /// <inheritdoc />
    public abstract class AbstractResourceExtractor : IResourceExtractor
    {
        /// <inheritdoc />
        public ResourceInfo Extract(IResourceContext context, string name)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(context, nameof(context)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            return Accepts(context.Location) ? DoExtract(context, name) : null;
        }

        /// <inheritdoc />
        #if NETSTANDARD || NET45_OR_NEWER
        public virtual async Task<ResourceInfo> ExtractAsync(IResourceContext context, string name)
        #elif NET35_OR_NEWER
        public virtual Task<ResourceInfo> ExtractAsync(IResourceContext context, string name)
        #endif
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(context, nameof(context)));
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(name, nameof(name)));
            if (!Accepts(context.Location))
            {
                #if NETSTANDARD || NET45_OR_NEWER
                return await Task.FromResult<ResourceInfo>(null);
                #elif NET40
                var taskCompletionSource = new TaskCompletionSource<ResourceInfo>();
                taskCompletionSource.SetResult(null);
                return taskCompletionSource.Task;
                #else
                return Task.FromResult<ResourceInfo>(null);
                #endif
            }
            #if NETSTANDARD || NET45_OR_NEWER
            return await Task.Run(() => DoExtract(context, name));
            #elif NET40
            return Task.Factory.StartNew(() => DoExtract(context, name));
            #else
            return Task.Run(() => DoExtract(context, name));
            #endif
        }

        /// <summary>
        /// Attempts to determine if the current <see cref="AbstractResourceExtractor"/> implementation is capable of
        /// extracting the resource represented by the provided <paramref name="location"/>.
        /// <para>
        /// This method is called prior to the <see cref="Extract"/> or <see cref="ExtractAsync"/> methods, and in case
        /// it returns <c>false</c>, the later may not be invoked.
        /// </para>
        /// </summary>
        /// <param name="location">
        /// The <see cref="Uri"/> representing the resource's location.
        /// </param>
        /// <returns>
        /// <c>true</c> if the current <see cref="AbstractResourceExtractor"/> implementation is capable of extracting
        /// the resource represented by the provided <paramref name="location"/>; <c>false</c> otherwise.
        /// </returns>
        protected virtual bool Accepts(Uri location) => true;

        /// <summary>
        /// When overriden in a derived class, this method performs the actual resource extraction logic for the current
        /// <see cref="AbstractResourceExtractor"/> implementation.
        /// </summary>
        /// <param name="context">
        /// A <see cref="IResourceContext"/> instance that represents the context of the current resource extraction. 
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> value used to identify the requested resource. 
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the extracted resource,
        /// or <c>null</c> if the resource was not found. 
        /// </returns>
        protected virtual ResourceInfo DoExtract(IResourceContext context, string name) => context.Extract(name);
    }
}