using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Http
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetCoreHttpAttribute : UtilizesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UtilizesAspNetCoreHttpAttribute"/> class. 
        /// </summary>
        public UtilizesAspNetCoreHttpAttribute() : base(typeof(AspNetCoreHttpModule)) { }
    }
}