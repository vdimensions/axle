using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Cors
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class UtilizesAspNetCoreCorsAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreCorsAttribute"/> class.
        /// </summary>
        public UtilizesAspNetCoreCorsAttribute() : base(typeof(AspNetCoreCorsModule)) { }
    }
}