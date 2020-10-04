using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Http
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreHttpModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreHttpAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreHttpAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreHttpAttribute() : base(typeof(AspNetCoreHttpModule)) { }
    }
}