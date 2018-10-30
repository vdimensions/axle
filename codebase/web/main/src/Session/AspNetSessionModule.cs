using System;
using System.Diagnostics.CodeAnalysis;

using Axle.Modularity;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore.Session
{

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetSessionAttribute : RequiresAttribute
    {
        public RequiresAspNetSessionAttribute() : base(typeof(AspNetSessionModule)) { }
    }

    [Module]
    [RequiresAspNetCore]
    public sealed class AspNetSessionModule : IServiceConfigurer, IApplicationConfigurer
    {
        private SessionLifetime _lt = new SessionLifetime(TimeSpan.FromMinutes(20));

        IServiceCollection IServiceConfigurer.Configure(IServiceCollection services)
        {
            return services.AddDistributedMemoryCache().AddSession();
        }

        Microsoft.AspNetCore.Builder.IApplicationBuilder IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            // TODO: subscribe events
            return app
                .UseSession()
                .Use(_lt.Middleware)
                ;
        }

        [ModuleTerminate]
        internal void Terminate()
        {
            _lt?.Dispose();
        }
    }
}