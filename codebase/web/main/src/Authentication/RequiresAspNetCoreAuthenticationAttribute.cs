using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Authentication
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreAuthenticationModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAuthenticationAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreAuthenticationAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreAuthenticationAttribute() : base(typeof(AspNetCoreAuthenticationModule)) { }
    }
}