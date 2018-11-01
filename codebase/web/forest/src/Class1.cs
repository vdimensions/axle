using Axle.Modularity;
using Axle.Web.AspNetCore.Session;


namespace Axle.Web.AspNetCore.Forest
{
    using RequiresForest = AAF::Axle.Application.Forest.RequiresForest;

    [Module]
    [RequiresForest]
    [RequiresAspNetSession]
    public class Class1
    {
    }
}
