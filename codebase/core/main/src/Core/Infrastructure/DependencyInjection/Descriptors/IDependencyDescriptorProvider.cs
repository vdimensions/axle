using System;
using System.Collections.Generic;


namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public interface IDependencyDescriptorProvider
    {
        IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type);
        IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type);
        IEnumerable<IFactoryDescriptor> GetFactories(Type type);
        bool DoDependenciesConverge(DependencyInfo factoryArgumentDependency, DependencyInfo classMemberDependency);
    }
}