using System;


namespace Axle.Resources.Bundling
{
    [Obsolete]
    public interface IResourceLocation
    {
        Uri Path { get; }
    }
}