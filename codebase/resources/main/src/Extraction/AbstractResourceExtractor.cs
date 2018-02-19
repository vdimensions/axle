using Axle.Verification;


namespace Axle.Resources.Extraction
{
    public abstract class AbstractResourceExtractor : AbstractResourceExtractor<ResourceInfo>
    {
        protected AbstractResourceExtractor()
        {
        }        
    }
    public abstract class AbstractResourceExtractor<T> : IResourceExtractor where T: ResourceInfo
    {
        protected AbstractResourceExtractor()
        {
        }

        public ResourceInfo Extract(ResourceContext context, string name)
        {
            return DoExtract(context.VerifyArgument(nameof(context)).IsNotNull(), name.VerifyArgument(nameof(name)).IsNotNullOrEmpty());
        }

        protected virtual T DoExtract(ResourceContext context, string name)
        {
            return (T) context.ExtractionChain.Extract(name);
        }
    }
}