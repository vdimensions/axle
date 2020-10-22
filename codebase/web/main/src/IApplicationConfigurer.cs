namespace Axle.Web.AspNetCore
{
    [RequiresAspNetCore]
    public interface IApplicationConfigurer
    {
        #if NETSTANDARD2_1_OR_NEWER
        void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env);
        #else
        void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env);
        #endif
    }
}