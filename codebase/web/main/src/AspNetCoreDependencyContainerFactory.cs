using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Axle.DependencyInjection;

namespace Axle.Web.AspNetCore
{
    internal sealed class AspNetCoreDependencyContainerFactory : IDependencyContainerFactory
    {
        private sealed class AspNetCoreDependencyContainer : IDependencyContainer
        {
            private readonly IDependencyContainer _dependencyContainer;
            private readonly ConcurrentDictionary<Type, object> _exportedObjects;

            public AspNetCoreDependencyContainer(
                    IDependencyContainer dependencyContainer, 
                    ConcurrentDictionary<Type, object> exportedObjects)
            {
                _dependencyContainer = dependencyContainer;
                _exportedObjects = exportedObjects;
            }

            IDependencyExporter IDependencyExporter.Export(object instance, string name)
            {
                var result = _dependencyContainer.Export(instance, name);
                var type = instance.GetType();
                if (string.IsNullOrEmpty(name) && !NotExportableAttribute.IsDefinedFor(type))
                {
                    if (type.IsPublic)
                    {
                        _exportedObjects.AddOrUpdate(type, instance, (_, __) => instance);
                    }
                    var interfacesToRemove = new List<Type>();
                    foreach (var ifc in type.GetInterfaces())
                    {
                        if (!ifc.IsPublic)
                        {
                            continue;
                        }
                        _exportedObjects.AddOrUpdate(
                            ifc, 
                            instance, 
                            (conflictingInterface, existing) =>
                            {
                                interfacesToRemove.Add(conflictingInterface);
                                return existing;
                            });
                    }

                    foreach (var conflictingInterface in interfacesToRemove)
                    {
                        _exportedObjects.TryRemove(conflictingInterface, out _);
                    }
                }
                return result;
            }

            IDependencyRegistry IDependencyRegistry.RegisterType(Type type, string name, params string[] aliases) 
                => _dependencyContainer.RegisterType(type, name, aliases);

            object IDependencyContext.Resolve(Type type, string name) 
                => _dependencyContainer.Resolve(type, name);

            IDependencyContainer IDependencyContainer.RegisterInstance(object instance, string name, params string[] aliases) 
                => _dependencyContainer.RegisterInstance(instance, name, aliases);

            IDependencyContext IDependencyContext.Parent => _dependencyContainer.Parent;

            void IDisposable.Dispose()
            {
                _dependencyContainer.Dispose();
                _exportedObjects.Clear();
            }
        }
        
        private readonly IDependencyContainerFactory _dependencyContainerFactory;
        private readonly AspNetCoreDependencyContainer _rootContainer;

        public AspNetCoreDependencyContainerFactory(
            IDependencyContainerFactory dependencyContainerFactory)
        {
            _dependencyContainerFactory = dependencyContainerFactory;
            _rootContainer = new AspNetCoreDependencyContainer(
                dependencyContainerFactory.CreateContainer(), 
                ExportedObjects = new ConcurrentDictionary<Type, object>());
        }

        IDependencyContainer IDependencyContainerFactory.CreateContainer() => _rootContainer;

        IDependencyContainer IDependencyContainerFactory.CreateContainer(IDependencyContext parent) 
            => _dependencyContainerFactory.CreateContainer(parent);

        public ConcurrentDictionary<Type, object> ExportedObjects { get; }
    }
}