using System.Collections.Generic;
using Axle.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Cors
{
    public sealed class AspNetCoreCorsConfig
    {
        public sealed class CorsFields
        {
            public List<string> Headers { get; set; } = new List<string>();
            public List<string> Origins { get; set; } = new List<string>();
            public List<string> Methods { get; set; } = new List<string>();
            public bool Credentials { get; set; }
        }
        
        public CorsFields Allowed { get; set; } = new CorsFields();
        
    }
    [RequiresAspNetCore]
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
            var policy = new CorsPolicy();
            _config.Allowed.Headers.ForEach(policy.Headers.Add);
            _config.Allowed.Origins.ForEach(policy.Origins.Add);
            _config.Allowed.Methods.ForEach(policy.Methods.Add);
            policy.SupportsCredentials = _config.Allowed.Credentials;
            corsOptions.AddPolicy(corsOptions.DefaultPolicyName, policy);
           
        }

        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors();
        }
    }
}