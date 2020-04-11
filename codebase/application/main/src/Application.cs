using System;
using System.Linq;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;


namespace Axle
{
    public sealed partial class Application : IDisposable
    {
        public static IApplicationBuilder Build() => new Builder();

        private readonly ModularityEngine _modularityEngine;
        private readonly IApplicationHost _host;
        private readonly IDependencyContainer _rootDependencyContainer;

        internal Application(IGrouping<int, ModuleInfo>[] rankedModules,
            IApplicationHost host,
            IDependencyContainer rootDependencyContainer,
            IConfigManager config,
            string[] args)
        {
            _host = host;
            (_rootDependencyContainer = rootDependencyContainer).Export(this);
            _modularityEngine = ModularityEngine.Launch(this, rankedModules, _host, _rootDependencyContainer, config, args);
        }

        public void Run() => _modularityEngine.Run();

        public void ShutDown() => _modularityEngine.Dispose();

        public IDependencyContainer CreateContainer() => CreateContainer(_rootDependencyContainer);
        public IDependencyContainer CreateContainer(IDependencyContainer parent) => _host.DependencyContainerFactory.CreateContainer(parent);

        void IDisposable.Dispose() => ShutDown();
    }
}
