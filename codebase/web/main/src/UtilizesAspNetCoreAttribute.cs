using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class UtilizesAspNetCoreAttribute : UtilizesAttribute
    {
        public UtilizesAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
}