using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore
{

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetSessionAttribute : RequiresAttribute
    {
        public RequiresAspNetSessionAttribute() : base(typeof(AspNetSessionModule)) { }
    }

    [Module]
    [RequiresAspNetCore]
    internal sealed class AspNetSessionModule : IServiceConfigurer, IApplicationConfigurer
    {
        IServiceCollection IServiceConfigurer.Configure(IServiceCollection services)
        {
            return services.AddDistributedMemoryCache().AddSession();
        }

        Microsoft.AspNetCore.Builder.IApplicationBuilder IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            return app.UseSession();
        }
    }
}