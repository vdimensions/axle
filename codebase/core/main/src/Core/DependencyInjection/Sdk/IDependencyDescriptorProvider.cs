using System;
using System.Collections.Generic;

using Axle.Core.DependencyInjection.Descriptors;


namespace Axle.Core.DependencyInjection.Sdk
{
    public interface IDependencyDescriptorProvider
    {
        IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type);
        IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type);
        IEnumerable<IFactoryDescriptor> GetFactories(Type type);
        bool DoDependenciesConverge(DependencyInfo factoryArgumentDependency, DependencyInfo classMemberDependency);
    }
}