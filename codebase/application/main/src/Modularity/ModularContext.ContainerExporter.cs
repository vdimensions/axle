using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif
//using System.Threading.Tasks;

using Axle.DependencyInjection;
using Axle.Extensions.String;
using Axle.Logging;


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
        }
    }
}