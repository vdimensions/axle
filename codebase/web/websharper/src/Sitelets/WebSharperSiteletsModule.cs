using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebSharper.AspNetCore;
using WebSharper.Sitelets;

namespace Axle.Web.WebSharper.Sitelets
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession]
    [RequiresWebSharper]
    internal sealed class WebSharperSiteletsModule : IApplicationConfigurer, ISiteletRegistry, IServiceConfigurer
    {
        private readonly IList<Action<IServiceCollection>> _siteletRegistrationRequests = new List<Action<IServiceCollection>>();
        private readonly IList<IWebSharperConfigurer> _wsConfigurers = new List<IWebSharperConfigurer>();

        private void ConfigureWebSharper(WebSharperBuilder b)
        {
            for (var i = 0; i < _wsConfigurers.Count; i++)
            {
                _wsConfigurers[i].Configure(b);
            }
        }

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void SiteletProviderInitialized(ISiteletProvider provider) => provider.RegisterSitelet(this);

        [ModuleDependencyInitialized]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void WebSharperConfigurerInitialized(IWebSharperConfigurer configurer) => _wsConfigurers.Add(configurer);

        [ModuleDependencyTerminated]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void WebSharperConfigurerTerminated(IWebSharperConfigurer configurer) => _wsConfigurers.Remove(configurer);

        [ModuleTerminate]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal void OnTerminated()
        {
            _siteletRegistrationRequests.Clear();
            _wsConfigurers.Clear();
        }

        ISiteletRegistry ISiteletRegistry.RegisterSitelet<T>(Sitelet<T> sitelet)
        {
            _siteletRegistrationRequests.Add(s => s.AddSitelet(sitelet));
            return this;
        }
        ISiteletRegistry ISiteletRegistry.RegisterSitelet<T>()
        {
            _siteletRegistrationRequests.Add(s => s.AddSitelet<T>());
            return this;
        }

        void IApplicationConfigurer.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment _) => app.UseWebSharper(ConfigureWebSharper);

        void IServiceConfigurer.Configure(IServiceCollection services)
        {
            for (var i = 0; i < _siteletRegistrationRequests.Count; i++)
            {
                _siteletRegistrationRequests[i].Invoke(services);
            }
            // free any references held up by the lambdas
            _siteletRegistrationRequests.Clear();
        }
    }
}