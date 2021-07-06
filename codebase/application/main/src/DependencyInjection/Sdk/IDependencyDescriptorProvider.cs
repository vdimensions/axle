using System;
using System.Collections.Generic;

using Axle.DependencyInjection.Descriptors;


namespace Axle.DependencyInjection.Sdk
{
    /// <summary>
    /// An interface representing a dependency descriptor.
    /// A dependency descriptor is responsible for extracting metadata from types and method parameters that is
    /// used by a dependency container. 
    /// </summary>
    public interface IDependencyDescriptorProvider
    {
        IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type);
        IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type);
        IEnumerable<IFactoryDescriptor> GetFactories(Type type);
        bool DoDependenciesConverge(DependencyInfo factoryArgumentDependency, DependencyInfo classMemberDependency);
    }
}