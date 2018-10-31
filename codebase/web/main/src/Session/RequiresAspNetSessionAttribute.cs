using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;


namespace Axle.Web.AspNetCore.Session
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetSessionAttribute : RequiresAttribute
    {
        public RequiresAspNetSessionAttribute() : base(typeof(AspNetSessionModule)) { }
    }
}