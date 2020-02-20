namespace Axle.Resources.Extraction
{
    public interface IResourceComposer
    {
        ResourceInfo Compose(ResourceInfo baseResource);
    }
}