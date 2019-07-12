using Axle.DependencyInjection;


namespace Axle.Modularity
{
    partial class ModularContext
    {
        private sealed class ContainerExporter : ModuleExporter
        {
            private readonly IContainer _container;

            public ContainerExporter(IContainer container)
            {
                _container = container;
            }

            public override ModuleExporter Export<T>(T instance, string name)
            {
                _container.RegisterInstance(instance, name);
                return this;
            }

            public override ModuleExporter Export<T>(T instance)
            {
                _container.RegisterInstance(instance);
                return this;
            }

            public override ModuleExporter Export<T>(string name)
            {
                _container.RegisterType<T>(name);
                return this;
            }

            public override ModuleExporter Export<T>()
            {
                _container.RegisterType<T>();
                return this;
            }
        }
    }
}