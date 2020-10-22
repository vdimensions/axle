using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Authentication
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreAuthenticationAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreAuthenticationAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreAuthenticationAttribute() : base(typeof(AspNetCoreAuthenticationModule)) { }
    }
}