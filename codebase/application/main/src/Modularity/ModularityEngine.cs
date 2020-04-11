using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Text.Expressions.Substitution;


namespace Axle.Modularity
{
    internal sealed partial class ModularityEngine : IDisposable
    {
        private readonly IDependencyContainer _rootDependencyContainer;
        private readonly ConcurrentStack<Type> _instantiatedModules;
        private readonly IList<Type> _initializedModules;
        private readonly ConcurrentDictionary<Type, ModuleContext> _modules;

        public static ModularityEngine Launch(
            Application application,
            IGrouping<int, ModuleInfo>[] rankedModules, 
            IApplicationHost host,
            IDependencyContainer rootDependencyContainer,
            IConfigManager config, 
            string[] args)
        {
            var modules = new ConcurrentDictionary<Type, ModuleContext>();
            var existingModuleTypes = new HashSet<Type>(modules.Keys);
            
            var substExpr = new StandardSubstitutionExpression();
            IConfiguration globalAppConfig = new SubstitutionResolvingConfig(config.LoadConfiguration(), substExpr);

            rootDependencyContainer.RegisterInstance(globalAppConfig);

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
                    using (var moduleInitializationContainer = host.DependencyContainerFactory.CreateContainer(rootDependencyContainer))
                    {
                        var moduleLogger = host.LoggingService.CreateLogger(moduleType);
                        var moduleContainer = host.DependencyContainerFactory.CreateContainer(rootDependencyContainer);
                        moduleInitializationContainer
                            .RegisterType(moduleType)          // register the module type to be instantiated via DI
                            .RegisterInstance(moduleInfo)      // register moduleInfo so that a module can reflect on itself
                            .RegisterInstance(moduleLogger)    // register the module's dedicated logger
                            .RegisterInstance(moduleContainer);// register the module's dedicated DI container
                            //TODO: remove
                            //.RegisterInstance(rootExporter);   // register the global dependencies exporter

                        //
                        // Make all required modules injectable
                        //
                        for (var k = 0; k < requiredModules.Length; k++)
                        {
                            if (modules.TryGetValue(requiredModules[k].Type, out var rmm))
                            {
                                moduleInitializationContainer.RegisterInstance(rmm.ModuleInstance);
                            }
                        }

                        var baseModuleConfig =
                            // TODO: prepend a module's embedded configuration file
                            // config.Prepend(...);
                            config;
                        var moduleConfig = 
                            new SubstitutionResolvingConfig(
                                baseModuleConfig.LoadConfiguration(),
                                substExpr);
                        moduleInitializationContainer.RegisterInstance(moduleConfig);

                        if (moduleInfo.ConfigSectionInfo != null)
                        {   //
                            // if the module expects a configuration section from the global config
                            // auto-resolve it here and make it available for injection into the module
                            //
                            var csi = moduleInfo.ConfigSectionInfo;
                            var moduleConfigSection = ConfigSectionExtensions.GetSection(moduleConfig, csi.Name, csi.Type);
                            if (moduleConfigSection != null)
                            {
                                moduleInitializationContainer.RegisterInstance(moduleConfigSection);
                            }
                        }

                        var moduleInstance = moduleInitializationContainer.Resolve(moduleType);
                        var moduleContext = modules.AddOrUpdate(
                            moduleType,
                            _ =>
                            {
                                var result = new ModuleContext(moduleInfo).UpdateInstance(moduleInstance, moduleInitializationContainer, rootDependencyContainer, moduleLogger, args);
                                instantiatedModules.Push(moduleType);
                                return result;
                            },
                            (mt, ctx) =>
                            {
                                if (ctx.ModuleInstance != null)
                                {
                                    return ctx;
                                }

                                var result = ctx.UpdateInstance(moduleInstance, moduleInitializationContainer, rootDependencyContainer, moduleLogger, args);
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

                            // TODO: throw proper exception
                            throw;
                        }
                    }
                }
            }

            return new ModularityEngine(rootDependencyContainer, instantiatedModules, initializedModules, modules, host);
        }

        private ModularityEngine(
            IDependencyContainer rootDependencyContainer,
            ConcurrentStack<Type> instantiatedModules,
            IList<Type> initializedModules,
            ConcurrentDictionary<Type, ModuleContext> modules, 
            IApplicationHost host)
        {
            _rootDependencyContainer = rootDependencyContainer;
            _instantiatedModules = instantiatedModules;
            _initializedModules = initializedModules;
            _modules = modules;
            Host = host;
        }

        public void Run()
        {
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
        
        public void Dispose()
        {
            while (_instantiatedModules.TryPop(out var moduleType))
            {
                _modules.AddOrUpdate(
                    moduleType,
                    (_) => throw new InvalidOperationException(),
                    (_, module) => module.Terminate(_modules));
            }
            _modules.Clear();
            _rootDependencyContainer?.Dispose();
            if (Host is IDisposable disposableHost)
            {
                disposableHost.Dispose();
            }
        }
        
        public IApplicationHost Host { get; }
    }
}