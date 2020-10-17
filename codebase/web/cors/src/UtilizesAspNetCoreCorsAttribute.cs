using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Cors
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class UtilizesAspNetCoreCorsAttribute : UtilizesAttribute
    {
        public UtilizesAspNetCoreCorsAttribute() : base(typeof(AspNetCoreCorsModule))
        {
        }
    }
}