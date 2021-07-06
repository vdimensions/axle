using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Authorization
{
    /// <summary>
    /// An attribute that, when applied on a module class, will establish the <see cref="AspNetCoreAuthorizationModule"/> as a
    /// dependency on the target module.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAuthorizationAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreAuthorizationAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreAuthorizationAttribute() : base(typeof(AspNetCoreAuthorizationModule)) { }
    }
}