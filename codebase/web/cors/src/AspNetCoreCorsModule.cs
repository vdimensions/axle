using Axle.Modularity;
using Axle.Web.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
#if NETSTANDARD2_1_OR_NEWER
#else
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Cors
{
    [RequiresAspNetCore]
    [UtilizesAspNetCoreRouting]
    [ModuleConfigSection(typeof(AspNetCoreCorsConfig), "cors")]
    internal sealed class AspNetCoreCorsModule : IServiceConfigurer, IApplicationConfigurer
    {
        private readonly AspNetCoreCorsConfig _config;

        public AspNetCoreCorsModule(AspNetCoreCorsConfig config)
        {
            _config = config;
        }
        public AspNetCoreCorsModule() : this(new AspNetCoreCorsConfig()) { }

        public void Configure(IServiceCollection services)
        {
            services.AddCors(Configure);
        }

        private void Configure(CorsOptions corsOptions)
        {
            var allowed = _config.Allowed;
            if (allowed != null)
            {
                var policy = new CorsPolicy();
                allowed.Headers.ForEach(policy.Headers.Add);
                allowed.Origins.ForEach(policy.Origins.Add);
                allowed.Methods.ForEach(policy.Methods.Add);
                policy.SupportsCredentials = allowed.Credentials;
                corsOptions.AddPolicy(corsOptions.DefaultPolicyName, policy);
            }           
        }

        #if NETSTANDARD2_1_OR_NEWER
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        #else
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment env)
        #endif
        {
            app.UseCors();
        }
    }
}