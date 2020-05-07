using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;

namespace Axle
{
    partial class Application
    {
        private sealed partial class Builder
        {
            private readonly object _syncRoot = new object();
            private readonly IModuleCatalog _moduleCatalog = new DefaultModuleCatalog();
            private readonly ICollection<Type> _moduleTypes = new HashSet<Type>();
            private readonly IList<Action<IDependencyContainer>> _onContainerReadyHandlers = new List<Action<IDependencyContainer>>();

            private LayeredConfigManager _config = new LayeredConfigManager();
            private IApplicationHost _host = DefaultApplicationHost.Instance;

            private Builder Load(IEnumerable<Type> types)
            {
                foreach (var type in types)
                {
                    if (type != null)
                    {
                        _moduleTypes.Add(type);
                    }
                }
                return this;
            }

            private void PrintLogo(IConfiguration config)
            {
                var logo = config.GetSections("axle.application.logo").Select(x => x.Value).ToArray();
                foreach (var logoLine in logo)
                {
                    Console.WriteLine(logoLine);
                }
            }

            public Application Run(params string[] args)
            {
                try 
                {
                    var rootContainer = _host.DependencyContainerFactory.CreateContainer();
                    rootContainer
                        .Export(new ApplicationContainerFactory(_host.DependencyContainerFactory, rootContainer))
                        .Export(_host.LoggingService)
                        .Export(_host);
                    var configMgr = new LayeredConfigManager()
                        .Append(EnvironmentConfigSource.Instance)
                        .Append(_host.HostConfiguration)
                        .Append(_config)
                        .Append(_host.AppConfiguration);
                    var config = configMgr.LoadConfiguration();
                    
                    PrintLogo(config);
                    
                    var modulesConfigSection = config.GetIncludeExcludeCollection<Type>("axle.application.modules");
                    foreach (var moduleType in modulesConfigSection.IncludeElements)
                    {
                        _moduleTypes.Add(moduleType);
                    }
                    foreach (var moduleType in modulesConfigSection.ExcludeElements)
                    {
                        _moduleTypes.Remove(moduleType);
                    }
                    
                    foreach (var onContainerReadyHandler in _onContainerReadyHandlers)
                    {
                        onContainerReadyHandler.Invoke(rootContainer);
                    }
                    var app = Launch(
                        new ApplicationModuleCatalog(_moduleCatalog), _moduleTypes, _host, rootContainer, config, args);
                    app.Run();
                    return app;
                }
                catch (Exception)
                {
                    if (_host is IDisposable d)
                    {
                        d.Dispose();
                    }
                    throw;
                }
            }
        }
    }
}
