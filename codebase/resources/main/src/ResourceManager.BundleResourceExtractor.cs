#if !NETSTANDARD || NETSTANDARD1_3_OR_NEWER
using Axle.Resources.Extraction;
using Axle.Verification;


namespace Axle.Resources
{
    partial class ResourceManager
    {
        private sealed class BundleResourceExtractor : IResourceExtractor
        {
            private readonly string _bundle;
            private readonly IResourceExtractor _originalExtractor;

            public BundleResourceExtractor(string bundle, IResourceExtractor originalExtractor)
            {
                _bundle = bundle;
                _originalExtractor = originalExtractor;
            }

            public ResourceInfo Extract(ResourceExtractionContext context, string name)
            {
                context.VerifyArgument(nameof(context)).IsNotNull();
                name.VerifyArgument(nameof(name)).IsNotNull();

                var resource = _originalExtractor.Extract(context, name);
                if (resource != null)
                {
                    resource.Bundle = _bundle;
                }
                return resource;
            }
        }
    }
}
#endif