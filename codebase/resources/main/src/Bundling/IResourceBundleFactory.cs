namespace Axle.Resources.Bundling
{
    public interface IResourceBundleFactory
    {
        IConfigurableBundleContent CreateBundleContent(string bundle);
    }
}