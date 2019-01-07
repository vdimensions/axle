﻿using System;
using System.Collections.Generic;

using Axle.Modularity;
using Axle.Web.AspNetCore;
using Axle.Web.AspNetCore.Session;

using Microsoft.Extensions.DependencyInjection;

using WebSharper.AspNetCore;
using WebSharper.Sitelets;


namespace Axle.Web.WebSharper
{
    [Module]
    [RequiresAspNetCore]
    [UtilizesAspNetSession]
    [RequiresWebSharper]
    internal sealed class WebSharperSiteletsModule : IApplicationConfigurer, ISiteletRegistry, IServiceConfigurer
    {
        private readonly IList<Action<IServiceCollection>> _siteletRegistrationRequests = new List<Action<IServiceCollection>>();

        public Microsoft.AspNetCore.Builder.IApplicationBuilder Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseWebSharper();
        }

        [ModuleDependencyInitialized]
        internal void SiteletProviderInitialized(ISiteletProvider provider) => provider.RegisterSitelets(this);

        [ModuleTerminate]
        internal void OnTerminated() => _siteletRegistrationRequests.Clear();

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
            // free any refernces held up by the lambdas
            _siteletRegistrationRequests.Clear();
            return builder;
        }
    }
}