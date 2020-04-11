using Axle.DependencyInjection;


namespace Axle.Modularity
{
    partial class ModularityEngine
    {
        private sealed class ContainerExporter : ModuleExporter
        {
            private readonly IDependencyContainer _dependencyContainer;

            public ContainerExporter(IDependencyContainer dependencyContainer)
            {
                _dependencyContainer = dependencyContainer;
            }

            public override ModuleExporter Export<T>(T instance, string name)
            {
                _dependencyContainer.RegisterInstance<T>(instance, name);
                return this;
            }

            public override ModuleExporter Export<T>(T instance)
            {
                _dependencyContainer.RegisterInstance(instance);
                return this;
            }

            public override ModuleExporter Export<T>(string name)
            {
                _dependencyContainer.RegisterType<T>(name);
                return this;
            }

            public override ModuleExporter Export<T>()
            {
                _dependencyContainer.RegisterType<T>();
                return this;
            }
        }
    }
}