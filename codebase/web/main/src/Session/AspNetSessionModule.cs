using System;

using Axle.Logging;
using Axle.Modularity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Axle.Web.AspNetCore.Session
{
    [Module]
    [RequiresAspNetCore]
    public sealed class AspNetSessionModule : IServiceConfigurer, IApplicationConfigurer, ISessionEventListener
    {
        private readonly SessionLifetime _lt = new SessionLifetime(TimeSpan.FromMinutes(20));
        private readonly ILogger _logger;

        public AspNetSessionModule(ILogger logger)
        {
            _logger = logger;
        }

        [ModuleInit]
        internal void Init()
        {
            _lt.Subscribe(this);
        }

        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            services.AddDistributedMemoryCache().AddSession();
        }

        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment _)
        {
            app.UseSession().Use(_lt.Middleware);
        }

        void ISessionEventListener.OnSessionStart(ISession session)
        {
            _logger.Trace("Started a new session '{0}'.", session.Id);
        }

        void ISessionEventListener.OnSessionEnd(string sessionId)
        {
            _logger.Trace("Session '{0}' has expired.", sessionId);
        }


        [ModuleDependencyInitialized]
        internal void OnSessionEventListenerInitialized(ISessionEventListener listener) => _lt.Subscribe(listener);

        [ModuleDependencyTerminated]
        internal void OnSessionEventListenerTerminated(ISessionEventListener listener) => _lt.Unsubscribe(listener);

        [ModuleTerminate]
        internal void Terminate() => _lt.Unsubscribe(this).Dispose();
    }
}