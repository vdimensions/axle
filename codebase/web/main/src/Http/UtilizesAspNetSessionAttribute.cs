using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Http
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class UtilizesAspNetHttpAttribute : UtilizesAttribute
    {
        public UtilizesAspNetHttpAttribute() : base(typeof(AspNetCoreHttpModule)) { }
    }
}