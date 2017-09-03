using System;
using System.Collections.Generic;


namespace Axle.DependencyInjection.Sdk
{
    public interface IDependencyDescriptorProvider
    {
        IEnumerable<FieldDependencyDescriptor> GetFields(Type type);
        IEnumerable<PropertyDependencyDescriptor> GetProperties(Type type);
        IEnumerable<FactoryDescriptor> GetFactories(Type type);
        bool DoDependenciesConverge(DependencyInfo factoryArgumentDependency, DependencyInfo classMemberDependency);
    }
}