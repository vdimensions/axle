using Axle.Verification;


namespace Axle.Resources.Extraction.ResX
{
    public sealed class ResXResourceExtractor : IResourceExtractor
    {
        public ResourceInfo Extract(ResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            IResourceExtractor actualExtractor = null;

            // ResX support varies depending on the platform
            #if !NETSTANDARD
            actualExtractor = new CompleteResXResourceExtractor();
            #elif NETSTANDARD2_0_OR_NEWER
            actualExtractor = new SimpleResXResourceExtractor();
            #elif NETSTANDARD1_3_OR_NEWER
            actualExtractor = new TextResXResourceExtractor();
            #endif

            return actualExtractor?.Extract(context, name);
        }
    }
}
