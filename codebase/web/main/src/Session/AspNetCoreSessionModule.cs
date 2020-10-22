using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Configuration;
using Axle.Logging;
using Axle.Modularity;
using Axle.Web.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Axle.Web.AspNetCore.Session
{
    [Module]
    [RequiresAspNetCore]
    [RequiresAspNetCoreHttp]
    internal sealed class AspNetCoreSessionModule : IServiceConfigurer, IApplicationConfigurer, ISessionEventListener, ISessionReferenceProvider
    {
        private readonly SessionScope _lt = new SessionScope(TimeSpan.FromMinutes(20));
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreSessionModule(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleInit]
        internal void Init()
        {
            _lt.Subscribe(this);
        }

        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            // TODO: move DistributedMemoryCache to separate module
            services
                .AddDistributedMemoryCache()
                .AddSession();
        }

        #if NETSTANDARD2_1_OR_NEWER
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        #else
        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        #endif
        {
            var options = new SessionOptions
            {
                Cookie =
                {
                    IsEssential = true
                }
            };
            app.UseSession(options).Use(_lt.Middleware);
        }

        void ISessionEventListener.OnSessionStart(ISession session) => _logger.Info("Started a new session '{0}'.", session.Id);

        void ISessionEventListener.OnSessionEnd(string sessionId) => _logger.Info("Session '{0}' has expired.", sessionId);
        
        SessionReference<T> ISessionReferenceProvider.CreateSessionReference<T>()
        {
            var sessionRef = new SessionReference<T>(_httpContextAccessor);
            _lt.Subscribe(sessionRef);
            return sessionRef;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnSessionEventListenerInitialized(ISessionEventListener listener) => _lt.Subscribe(listener);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnSessionEventListenerTerminated(ISessionEventListener listener) => _lt.Unsubscribe(listener);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleTerminate]
        internal void Terminate() => _lt.Unsubscribe(this).Dispose();
    }
}