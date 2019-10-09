using System;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}