﻿using System;
using System.Collections.Generic;

using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;

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

        public Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseWebSharper(ConfigureWebSharper);
        }

        private void ConfigureWebSharper(WebSharperBuilder b)
        {
            for (var i = 0; i < _wsConfigurers.Count; i++)
            {
                _wsConfigurers[i].Configure(b);
            }
        }

        [ModuleDependencyInitialized]
        internal void SiteletProviderInitialized(ISiteletProvider provider) => provider.RegisterSitelet(this);

        [ModuleDependencyInitialized]
        internal void WebSharperConfigurerInitialized(IWebSharperConfigurer configurer) => _wsConfigurers.Add(configurer);

        [ModuleDependencyTerminated]
        internal void WebSharperConfigurerTerminated(IWebSharperConfigurer configurer) => _wsConfigurers.Remove(configurer);

        [ModuleTerminate]
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

        IServiceCollection IServiceConfigurer.Configure(IServiceCollection builder)
        {
            for (var i = 0; i < _siteletRegistrationRequests.Count; i++)
            {
                _siteletRegistrationRequests[i].Invoke(builder);
            }
            // free any references held up by the lambdas
            _siteletRegistrationRequests.Clear();
            return builder;
        }
    }
}