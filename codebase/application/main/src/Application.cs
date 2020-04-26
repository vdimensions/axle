using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Modularity;
using Axle.Resources;
using Axle.Resources.Bundling;
using Axle.Text.Expressions.Substitution;

namespace Axle
{
    public sealed partial class Application : IDisposable
    {
        internal const string ConfigBundleName = "$Config";
        
        public static IApplicationBuilder Build() => new Builder();

        private readonly IDependencyContainer _rootContainer;
        private readonly ConcurrentStack<Type> _instantiatedModules;
        private readonly IList<Type> _initializedModules;
        private readonly ConcurrentDictionary<Type, ModuleWrapper> _modules;

        internal static Stream LoadResourceConfig(ResourceManager resourceManager, string prefix, string configFile)
        {
            return resourceManager.Load(ConfigBundleName, Path.Combine(prefix, configFile), CultureInfo.InvariantCulture)?.Open();
        }

        internal static Application Launch(
            IModuleCatalog moduleCatalog,
            IEnumerable<Type> moduleTypes, 
            IApplicationHost host,
            IDependencyContainer rootContainer,
            LayeredConfigManager config, 
            string[] args)
        {
            var loadedModules = moduleCatalog.GetModules(
                moduleTypes.ToArray(),
                host.GetType(), 
                args);
            
            var rankedModules = moduleCatalog
                .RankModules(loadedModules, new List<Type>())
                .ToArray();
            
            var modules = new ConcurrentDictionary<Type, ModuleWrapper>();
            var existingModuleTypes = new HashSet<Type>(modules.Keys);
            
            var substExpr = new StandardSubstitutionExpression();
            IConfiguration globalAppConfig = new SubstitutionResolvingConfig(config.LoadConfiguration(), substExpr);

            rootContainer.Export(globalAppConfig);

            // TODO: strip ranked modules from non-activated
            
            var initializedModules = new List<Type>();
            var instantiatedModules = new ConcurrentStack<Type>();
            for (var i = 0; i < rankedModules.Length; i++)
            {
                foreach (var moduleInfo in rankedModules[i])
                {
                    var moduleType = moduleInfo.Type;
                    if (existingModuleTypes.Contains(moduleType))
                    {   //
                        // the module was initialized before
                        //
                        continue;
                    }

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
                        moduleResourceManager.Bundles.Configure(ConfigBundleName).Register(moduleAssembly);
                        
                        var generalConfig = Configure(
                            new LayeredConfigManager(), 
                            f => LoadResourceConfig(moduleResourceManager, moduleTypeName, f), 
                            string.Empty);
                        var envSpecificConfig = Configure(
                            new LayeredConfigManager(), 
                            f => LoadResourceConfig(moduleResourceManager, moduleTypeName, f), 
                            host.EnvironmentName);

                        var baseModuleConfig = config.Prepend(envSpecificConfig).Prepend(generalConfig);
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

        public void Run()
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
        
        public IApplicationHost Host { get; }
    }
}
