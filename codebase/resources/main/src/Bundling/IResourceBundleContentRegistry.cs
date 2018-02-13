using System;
using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    public interface IResourceBundleContentRegistry : IEnumerable<Uri>
    {
        IResourceBundleContentRegistry Register(Uri location);
    }
}