using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Web.AspNetCore.Sdk
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal abstract class AbstractConfigurableAspNetCoreModule<TConfigurer, TOptions> where TConfigurer: IAspNetCoreConfigurer<TOptions>
    {
        private readonly IList<TConfigurer> _configurers = new List<TConfigurer>();
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(TConfigurer cfg) => _configurers.Add(cfg);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(TConfigurer cfg) => _configurers.Remove(cfg);
        
        protected void Configure(TOptions options)
        {
            foreach (var configurer in _configurers)
            {
                configurer.Configure(options);
            }
        }
    }
    
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal abstract class AbstractConfigurableAspNetCoreModule<TConfigurer, TContext, TOptions> where TConfigurer: IAspNetCoreConfigurer<TContext, TOptions>
    {
        private readonly IList<TConfigurer> _configurers = new List<TConfigurer>();
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyInitialized]
        internal void OnDependencyInitialized(TConfigurer cfg) => _configurers.Add(cfg);
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [ModuleDependencyTerminated]
        internal void OnDependencyTerminated(TConfigurer cfg) => _configurers.Remove(cfg);
        
        protected void Configure(TContext context, TOptions options)
        {
            foreach (var configurer in _configurers)
            {
                configurer.Configure(context, options);
            }
        }
    }
}