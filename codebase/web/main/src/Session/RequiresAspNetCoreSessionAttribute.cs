using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Session
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreSessionAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAspNetCoreSessionAttribute"/> class. 
        /// </summary>
        public RequiresAspNetCoreSessionAttribute() : base(typeof(AspNetCoreSessionModule)) { }
    }
}