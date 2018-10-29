using System;
using System.Collections.Generic;

using Axle.Modularity;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresWebHostAttribute : RequiresAttribute
    {
        public RequiresWebHostAttribute() : base(typeof(WebHostModule)) { }
    }

    [RequiresWebHost]
    public interface IWebHostConfigurer
    {
        IWebHostBuilder Configure(IWebHostBuilder builder);
    }
    [RequiresWebHost]
    public interface IServiceConfigurer
    {
        IServiceCollection Configure(IServiceCollection builder);
    }
    [RequiresWebHost]
    public interface IApplicationConfigurer
    {
        Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder);
    }

    [Module]
    internal sealed class WebHostModule
    {
        private readonly IList<IWebHostConfigurer> _whConfigurers = new List<IWebHostConfigurer>();
        private readonly IList<IServiceConfigurer> _serviceConfigurers = new List<IServiceConfigurer>();
        private readonly IList<IApplicationConfigurer> _appConfigurers = new List<IApplicationConfigurer>();

        [ModuleInit]
        internal void Init()
        {
        }

        [ModuleDependencyInitialized]
        internal void OnWebHostConfigurerInitialized(IWebHostConfigurer cfg)
        {
            _whConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        internal void OnServiceConfigurerInitialized(IServiceConfigurer cfg)
        {
            _serviceConfigurers.Add(cfg);
        }

        [ModuleDependencyInitialized]
        internal void OnApplicationConfigurerInitialized(IApplicationConfigurer cfg)
        {
            _appConfigurers.Add(cfg);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            foreach (var cfg in _serviceConfigurers) cfg.Configure(services);
        }

        private void ConfigureApp(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            foreach (var cfg in _appConfigurers) cfg.Configure(app);
        }

        [ModuleEntryPoint]
        internal void Run(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args);

            foreach (var cfg in _whConfigurers)
            {
                cfg.Configure(host);
            }

            host
                .ConfigureServices(ConfigureServices)
                .Configure(ConfigureApp)
                .Build()
                .Run();
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class RequiresAspNetCoreAttribute : RequiresAttribute
    {
        public RequiresAspNetCoreAttribute() : base(typeof(AspNetCoreModule)) { }
    }
    [Module]
    [RequiresWebHost]
    internal sealed class AspNetCoreModule
    {
    }

    
}
