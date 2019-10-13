using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Mvc
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetMvcAttribute : RequiresAttribute
    {
        public RequiresAspNetMvcAttribute() : base(typeof(AspNetMvcModule)) { }
    }
}