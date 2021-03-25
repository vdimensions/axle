using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Http
{
    [Module]
    internal sealed class AspNetCoreHttpModule : IServiceConfigurer, IApplicationConfigurer
    {
        private readonly AxleHttpContextAccessor _httpContextAccessor = new AxleHttpContextAccessor();
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleInit]
        internal void Init(IDependencyExporter exporter)
        {
            exporter.Export<IHttpContextAccessor>(_httpContextAccessor);
        }
        
        public void Configure(IServiceCollection services) => services.AddHttpContextAccessor();
        
        #if NETCOREAPP3_0_OR_NEWER
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        #else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        #endif
        {
            _httpContextAccessor.Accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        }
    }
}