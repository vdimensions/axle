namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IApplicationConfigurer
    {
        #if NETCOREAPP3_0_OR_NEWER
        void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env);
        #else
        void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env);
        #endif
    }
}