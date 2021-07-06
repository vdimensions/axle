namespace Axle.Resources.Bundling
{
    public abstract class ResourceBundleFactoryDecorator : IResourceBundleFactory
    {
        private readonly IResourceBundleFactory _impl;

        protected ResourceBundleFactoryDecorator(IResourceBundleFactory impl) => _impl = impl;

        public virtual IConfigurableBundleContent CreateBundleContent(string bundle) => _impl.CreateBundleContent(bundle);
    }
}