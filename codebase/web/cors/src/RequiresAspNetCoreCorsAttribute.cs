using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Cors
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreCorsModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresAspNetCoreCorsAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreCorsAttribute"/> class.
        /// </summary>
        public RequiresAspNetCoreCorsAttribute() : base(typeof(AspNetCoreCorsModule))
        {
        }
    }
}