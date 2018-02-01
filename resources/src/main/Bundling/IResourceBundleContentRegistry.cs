using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    public interface IResourceBundleContentRegistry : IEnumerable<IResourceLocation>
    {
        IResourceBundleContentRegistry Register(IResourceLocation location);
    }
}