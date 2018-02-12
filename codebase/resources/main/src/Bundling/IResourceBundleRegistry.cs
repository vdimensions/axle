using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    public interface IResourceBundleRegistry : IEnumerable<IResourceBundleContentRegistry>
    {
        IResourceBundleContentRegistry Configure(string bundle);

        IResourceBundleContentRegistry this[string bundle] { get; }
    }
}