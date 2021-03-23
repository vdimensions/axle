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
using Axle.Web.AspNetCore.Mvc.RazorPages;
using Axle.Web.AspNetCore.Mvc.Routing;
using Axle.Web.AspNetCore.Routing;
using Axle.Web.AspNetCore.Sdk;
using Axle.Web.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private readonly IList<IRouteBuilderConfigurer> _routeBuilderConfigurers = new List<IRouteBuilderConfigurer>();
        private readonly IList<IAspNetCoreConfigurer<IMvcBuilder>> _mvcBuilderConfigurers = new List<IAspNetCoreConfigurer<IMvcBuilder>>();
        private readonly IList<IAspNetCoreConfigurer<MvcOptions>> _mvcConfigurers = new List<IAspNetCoreConfigurer<MvcOptions>>();
        private readonly IList<IAspNetCoreConfigurer<RazorPagesOptions>> _razorPagesConfigurers = new List<IAspNetCoreConfigurer<RazorPagesOptions>>();
        private readonly IList<IModelResolverProvider> _modelResolverProviders = new List<IModelResolverProvider>();
        private readonly ICollection<Type> _modelResolverTypes = new HashSet<Type>();

        public AspNetCoreMvcModule(ILogger logger)
        {
            _logger = logger;
        }
        
        #region Initialize
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IRouteBuilderConfigurer configurer) => _routeBuilderConfigurers.Add(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcBuilderConfigurer configurer) => _mvcBuilderConfigurers.Add(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IMvcConfigurer configurer) => _mvcConfigurers.Add(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IRazorPagesConfigurer configurer) => _razorPagesConfigurers.Add(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(IModelResolverProvider configurer) => _modelResolverProviders.Add(configurer);
        #endregion Initialize
        
        #region Terminate
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IRouteBuilderConfigurer configurer) => _routeBuilderConfigurers.Remove(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcBuilderConfigurer configurer) => _mvcBuilderConfigurers.Remove(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IMvcConfigurer configurer) => _mvcConfigurers.Remove(configurer);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IRazorPagesConfigurer configurer) => _razorPagesConfigurers.Remove(configurer);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(IModelResolverProvider configurer) => _modelResolverProviders.Remove(configurer);
        #endregion Terminate
        
        void IServiceConfigurer.Configure(IServiceCollection services) 
        {
            // SEE: https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.1&tabs=visual-studio
            // SEE: https://dotnettutorials.net/lesson/difference-between-addmvc-and-addmvccore-method/

            var razorPagesConfigured = false;
            #if NETSTANDARD2_1_OR_NEWER
            IMvcBuilder builder;
            if (_mvcBuilderConfigurers.All(c => c is IRazorPagesBuilderConfigurer))
            {
                builder = services.AddRazorPages(ConfigureRazorPages).AddMvcOptions(ConfigureMvc);
                razorPagesConfigured = true;
            }
            else if (_mvcBuilderConfigurers.Any(c => c is IMvcBuilderConfigurer))
            {
                builder = services.AddMvc(ConfigureMvc);
            }
            else if (_mvcBuilderConfigurers.Any(c => c is IControllersWithViewsConfigurer))
            {
                builder = services.AddControllersWithViews(ConfigureMvc);
            }
            else
            {
                builder = services.AddControllers(ConfigureMvc);
            }
            #else
            var builder = services.AddMvc(ConfigureMvc);
            #endif
            
            if (_razorPagesConfigurers.Count > 0 & !razorPagesConfigured)
            {
                services.AddRazorPages(ConfigureRazorPages);
            }
            
            foreach (var configurer in _mvcBuilderConfigurers)
            {
                configurer.Configure(builder);
            }
        }
        
        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IWebHostEnvironment _)
        #else
        void IApplicationConfigurer.Configure(IApplicationBuilder app, IHostingEnvironment _)
        #endif
        {
            app.UseMvc(ConfigureRouteBuilder);
        }
        
        private void ConfigureRazorPages(RazorPagesOptions options)
        {
            foreach (var configurer in _razorPagesConfigurers)
            {
                configurer.Configure(options);
            }
        }
        
        private void ConfigureMvc(MvcOptions options)
        {
            #if NETSTANDARD2_1_OR_NEWER
            options.EnableEndpointRouting = false;
            #endif
            
            foreach (var configurer in _mvcConfigurers)
            {
                configurer.Configure(options);
            }
            
            foreach (var modelResolverProvider in _modelResolverProviders)
            {
                modelResolverProvider.RegisterTypes(this);
            }
            var bp = options.ModelBinderProviders.ToArray();
            //options.ModelBinderProviders.Clear();
            options.ModelBinderProviders.Insert(0, new AxleModelBinderProvider(_modelResolverTypes, _modelResolverProviders, bp));
        }

        private void ConfigureRouteBuilder(IRouteBuilder routeBuilder)
        {
            foreach (var configurer in _routeBuilderConfigurers)
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