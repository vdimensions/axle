using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.Logging;
using Axle.Modularity;
using Axle.Web.AspNetCore.Authentication;
using Axle.Web.AspNetCore.Authorization;
using Axle.Web.AspNetCore.Cors;
using Axle.Web.AspNetCore.Mvc.ModelBinding;
using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Mvc
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetCoreSession]             // If Session is used, MVC must be initialized after Session
    [UtilizesAspNetCoreCors]                // If Cors is used, MVC must be initialized after Cors
    [UtilizesAspNetCoreAuthentication]      // If Authentication is used, MVC must be initialized after Authentication
    [UtilizesAspNetCoreAuthorization]       // If Authorization is used, MVC must be initialized after Authorization
    [RequiresAspNetCoreRouting]
    public sealed class AspNetCoreMvcModule : IServiceConfigurer, IApplicationConfigurer, IModelTypeRegistry
    {
        private readonly ILogger _logger;
        private readonly IList<IMvcConfigurer> _configurers;
        private readonly IList<IMvcRouteConfigurer> _routeConfigurers;
        private readonly IList<IModelResolverProvider> _modelResolverProviders;
        private readonly ICollection<Type> _modelResolverTypes = new HashSet<Type>();


        public AspNetCoreMvcModule(ILogger logger)
        {
            _logger = logger;
            _configurers = new List<IMvcConfigurer>();
            _routeConfigurers = new List<IMvcRouteConfigurer>();
            _modelResolverProviders = new List<IModelResolverProvider>();
        }

        [ModuleInit]
        internal void Init()
        {
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcConfigurer configurer) => _configurers.Add(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcRouteConfigurer configurer) => _routeConfigurers.Add(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IModelResolverProvider configurer) => _modelResolverProviders.Add(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcConfigurer configurer) => _configurers.Remove(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcRouteConfigurer configurer) => _routeConfigurers.Remove(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IModelResolverProvider configurer) => _modelResolverProviders.Remove(configurer);

        #if NETSTANDARD2_1_OR_NEWER
        void IServiceConfigurer.Configure(IServiceCollection services) 
        {
            // SEE: https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.1&tabs=visual-studio

            // TODO
            // services.AddControllers();
            // services.AddControllersWithViews();
            // services.AddRazorPages();
            Configure(services.AddMvc());
        }
        #else
        void IServiceConfigurer.Configure(IServiceCollection services) => Configure(services.AddMvc());
        #endif
        
        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseMvc();
        }
        #else
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment _)
        {
            app.UseMvc(Configure);
        }
        #endif
        
        private void Configure(IMvcBuilder builder)
        {
            #if NETSTANDARD2_1_OR_NEWER
            builder.AddMvcOptions(options => options.EnableEndpointRouting = false);
            #endif
            
            foreach (var configurer in _configurers)
            {
                configurer.ConfigureMvc(builder);
            }

            builder.AddMvcOptions(OverrideModelBinding);
        }

        private void OverrideModelBinding(MvcOptions options)
        {
            foreach (var modelResolverProvider in _modelResolverProviders)
            {
                modelResolverProvider.RegisterTypes(this);
            }
            var bp = options.ModelBinderProviders.ToArray();
            //options.ModelBinderProviders.Clear();
            options.ModelBinderProviders.Insert(0, new AxleModelBinderProvider(_modelResolverTypes, _modelResolverProviders, bp));
        }

        private void Configure(IRouteBuilder routeBuilder)
        {
            foreach (var configurer in _routeConfigurers)
            {
                configurer.ConfigureRoutes(routeBuilder);
            }
        }

        void IModelTypeRegistry.Register(Type type) => _modelResolverTypes.Add(type);

        [ModuleTerminate]
        internal void Terminate()
        {
        }
    }
}