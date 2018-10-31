using Axle.Modularity;
using Axle.Web.AspNetCore.Session;


namespace Axle.Web.AspNetCore.Forest
{
    using RequiresForest = global::Axle.Application.Forest.RequiresForest;

    [Module]
    [RequiresForest]
    [RequiresAspNetSession]
    public class Class1
    {
    }
}
