using System;
using System.Collections.Generic;
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

            private void PrintLogo()
            {
                foreach (var logoLine in _host.AsciiLogo)
                {
                    Console.WriteLine(logoLine);
                }
            }

            public Application Run(params string[] args)
            {
                PrintLogo();
                try 
                {
                    var rootContainer = _host.DependencyContainerFactory.CreateContainer();
                    rootContainer
                        .Export(new ApplicationContainerFactory(_host.DependencyContainerFactory, rootContainer))
                        .Export(_host.LoggingService)
                        .Export(_host);
                    var configMgr = new LayeredConfigManager()
                        .Append(EnvironmentConfigSource.Instance)
                        .Append(new PreloadedConfigSource(_host.Configuration))
                        .Append(_config)
                        .Append(Configure(new LayeredConfigManager(), _host.ApplicationConfigFileName, this, string.Empty));
                    if (!string.IsNullOrEmpty(_host.EnvironmentName))
                    {
                        configMgr = configMgr.Append(Configure(new LayeredConfigManager(), _host.ApplicationConfigFileName, this, _host.EnvironmentName));
                    }
                    var config = configMgr.LoadConfiguration();
                    
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
