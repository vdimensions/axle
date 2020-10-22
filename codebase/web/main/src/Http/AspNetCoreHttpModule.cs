using System.Diagnostics.CodeAnalysis;
using Axle.DependencyInjection;
using Axle.Modularity;
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

        public void Configure(
            Microsoft.AspNetCore.Builder.IApplicationBuilder app,
            #if NETSTANDARD2_1_OR_NEWER
            IWebHostEnvironment env
            #else
            IHostingEnvironment env
            #endif
            )
        {
            _httpContextAccessor.Accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        }
    }
}