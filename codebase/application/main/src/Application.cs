using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;
using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Resources.Extraction;
using Axle.Text.Expressions.Substitution;

namespace Axle
{
    /// <summary>
    /// A representing an axle application.
    /// </summary>
    public sealed partial class Application : IDisposable, IDependencyContext
    {
        internal const string ConfigBundleName = "$Config";
        
        /// <summary>
        /// Initiates the configuration of an axle <see cref="Application">application</see> by providing a reference to
        /// an <see cref="IApplicationBuilder"/> instance.
        /// </summary>
        /// <returns>
        /// An <see cref="IApplicationBuilder"/> instance that is used to configure and run an axle
        /// <see cref="Application">application</see>.
        /// </returns>
        public static IApplicationBuilder Build() => new Builder();

        private readonly IDependencyContainer _rootContainer;
        private readonly ConcurrentStack<Type> _instantiatedModules;
        private readonly IList<Type> _initializedModules;
        private readonly ConcurrentDictionary<Type, ModuleWrapper> _modules;

        internal static Application Launch(
            IModuleCatalog moduleCatalog,
            IEnumerable<Type> moduleTypes, 
            IApplicationHost host,
            IDependencyContainer rootContainer,
            IConfiguration config, 
            string[] args)
        {
            var rankedModules = moduleCatalog
                .RankModules(moduleCatalog.GetModules(
                    moduleTypes.ToArray(),
                    host.GetType(), 
                    args))
                .ToArray();
            
            var modules = new ConcurrentDictionary<Type, ModuleWrapper>();
            var substExpr = new StandardSubstitutionExpression();
            IConfiguration globalAppConfig = new SubstitutionResolvingConfig(config, substExpr);

            rootContainer.Export(globalAppConfig);

            var initializedModules = new List<Type>();
            var instantiatedModules = new ConcurrentStack<Type>();
            for (var i = 0; i < rankedModules.Length; i++)
            {
                foreach (var moduleInfo in rankedModules[i])
                {
                    var moduleType = moduleInfo.Type;
                    var requiredModules = moduleInfo.RequiredModules.ToArray();
                    using (var moduleInitializationContainer = host.DependencyContainerFactory.CreateContainer(rootContainer))
                    {
                        var moduleLogger = host.LoggingService.CreateLogger(moduleType);
                        var moduleContainer = host.DependencyContainerFactory.CreateContainer(rootContainer);
                        moduleInitializationContainer
                            // Register the module type to be instantiated via DI
                            .RegisterType(moduleType);
                        moduleInitializationContainer
                            // Register moduleInfo so that a module can reflect on itself
                            .Export(moduleInfo)
                            // Register the module's dedicated logger
                            .Export(moduleLogger)
                            // Register the module's dedicated DI container.
                            // The `AsDependencyContext` ensures that the context will not be in conflict
                            // with the exporter defined below.
                            .Export(moduleContainer.AsDependencyContext())
                            // Register the root container as dependency exporter.
                            // The `AsDependencyExporter` ensures that the exporter will not be in conflict
                            // with the context defined above.
                            .Export(rootContainer.AsDependencyExporter());    

                        //
                        // Make all required modules injectable
                        //
                        for (var k = 0; k < requiredModules.Length; k++)
                        {
                            if (modules.TryGetValue(requiredModules[k].Type, out var rmm))
                            {
                                moduleInitializationContainer.Export(rmm.ModuleInstance);
                            }
                        }

                        var moduleAssembly = 
                            moduleType
                            #if NETSTANDARD || NET45_OR_NEWER
                            .GetTypeInfo()
                            #endif
                            .Assembly;
                        var moduleTypeName = 
                            moduleType
                            #if NETSTANDARD || NET45_OR_NEWER
                            .GetTypeInfo()
                            #endif
                            .Name;
                        var moduleResourceManager = new DefaultResourceManager();
                        moduleResourceManager.Bundles
                            .Configure(ConfigBundleName)
                            .Register(moduleAssembly)
                            .Extractors.Register(new PathForwardingResourceExtractor(moduleTypeName));

                        var moduleConfigurationStreamProvider = new ResourceConfigurationStreamProvider(moduleResourceManager);
                        
                        var generalConfig = Configure(new LayeredConfigManager(), moduleConfigurationStreamProvider, string.Empty);
                        var envSpecificConfig = Configure(
                            new LayeredConfigManager(), 
                            moduleConfigurationStreamProvider, 
                            host.EnvironmentName);

                        var baseModuleConfig = new LayeredConfigManager()
                            .Append(new PreloadedConfigSource(config))
                            .Append(generalConfig)
                            .Append(envSpecificConfig);
                        var moduleConfig = new SubstitutionResolvingConfig(baseModuleConfig.LoadConfiguration(), substExpr);
                        moduleInitializationContainer.Export(moduleConfig);

                        if (moduleInfo.ConfigSectionInfo != null)
                        {   //
                            // if the module expects a configuration section from the global config
                            // auto-resolve it here and make it available for injection into the module
                            //
                            var csi = moduleInfo.ConfigSectionInfo;
                            var moduleConfigSection = ConfigSectionExtensions.GetSection(moduleConfig, csi.Name, csi.Type);
                            if (moduleConfigSection != null)
                            {
                                moduleInitializationContainer.Export(moduleConfigSection);
                            }
                        }

                        var moduleInstance = moduleInitializationContainer.Resolve(moduleType);
                        var moduleContext = modules.AddOrUpdate(
                            moduleType,
                            _ =>
                            {
                                var result = new ModuleWrapper(moduleInfo).UpdateInstance(moduleInstance, moduleInitializationContainer, rootContainer, moduleLogger, args);
                                instantiatedModules.Push(moduleType);
                                return result;
                            },
                            (mt, ctx) =>
                            {
                                if (ctx.ModuleInstance != null)
                                {
                                    return ctx;
                                }

                                var result = ctx.UpdateInstance(moduleInstance, moduleInitializationContainer, rootContainer, moduleLogger, args);
                                instantiatedModules.Push(mt);
                                return result;
                            });

                        try
                        {
                            moduleContext = modules.AddOrUpdate(
                                moduleType, 
                                _ => moduleContext.Init(modules), 
                                (_, ctx) => ctx.Init(modules));
                            initializedModules.Add(moduleType);
                        }
                        catch
                        {
                            moduleContext = moduleContext.Terminate(modules);
                            if (moduleContext.ModuleInstance is IDisposable disposable)
                            {
                                disposable.Dispose();
                            }

                            // TODO: throw better exception
                            throw;
                        }
                    }
                }
            }
            
            return new Application(rootContainer, instantiatedModules, initializedModules, modules, host);
        }

        private Application(
            IDependencyContainer rootContainer,
            ConcurrentStack<Type> instantiatedModules,
            IList<Type> initializedModules,
            ConcurrentDictionary<Type, ModuleWrapper> modules, 
            IApplicationHost host)
        {
            _rootContainer = rootContainer;
            _instantiatedModules = instantiatedModules;
            _initializedModules = initializedModules;
            _modules = modules;
            Host = host;
        }

        private void Run()
        {
            foreach (var initializedModule in _initializedModules)
            {
                _modules.AddOrUpdate(
                    initializedModule,
                    _ => throw new InvalidOperationException(),
                    (_, m) => m.Prepare());
            }
            
            // TODO: runnable modules cause blocking. We need to delegate running to the app host
            foreach (var initializedModule in _initializedModules)
            {
                _modules.AddOrUpdate(
                    initializedModule,
                    _ => throw new InvalidOperationException(),
                    (_, m) =>  m.Run());
            }
            _initializedModules.Clear();
        }
        
        /// <summary>
        /// Shuts down the current <see cref="Application"/> instance, gracefully disposing of its allocated resources.
        /// </summary>
        public void ShutDown()
        {
            while (_instantiatedModules.TryPop(out var moduleType))
            {
                _modules.AddOrUpdate(
                    moduleType,
                    (_) => throw new InvalidOperationException(),
                    (_, module) => module.Terminate(_modules));
            }
            _modules.Clear();
            _rootContainer?.Dispose();
            if (Host is IDisposable disposableHost)
            {
                disposableHost.Dispose();
            }
        }

        void IDisposable.Dispose() => ShutDown();

        object IDependencyContext.Resolve(Type type, string name) => _rootContainer.Resolve(type, name);
        IDependencyContext IDependencyContext.Parent => null;
        
        /// <summary>
        /// Gets a reference to the <see cref="IApplicationHost"/> responsible for supplying the
        /// <see cref="Application">application</see> with environment settings and basic building blocks,
        /// such as a <see cref="IDependencyContainerFactory">dependency container factory</see> and
        /// <see cref="Axle.Logging.ILoggingService">logging support</see>. 
        /// </summary>
        internal IApplicationHost Host { get; }
    }
}
