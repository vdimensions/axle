using System;
using System.Collections.Generic;


namespace Axle.Resources.Bundling
{
    public interface IResourceBundleRegistry : IEnumerable<IResourceBundleContentRegistry>
    {
        IResourceBundleContentRegistry Configure(string bundle);

        IEnumerable<Uri> this[string bundle] { get; }
    }
}