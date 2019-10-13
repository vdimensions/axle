using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Mvc
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetMvcAttribute : UtilizesAttribute
    {
        public UtilizesAspNetMvcAttribute() : base(typeof(AspNetMvcModule)) { }
    }
}