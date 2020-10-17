using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Cors
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresAspNetCoreCorsAttribute : UtilizesAttribute
    {
        public RequiresAspNetCoreCorsAttribute() : base(typeof(AspNetCoreCorsModule))
        {
        }
    }
}