using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractDependencyDescriptorProvider : IDependencyDescriptorProvider
    {
        protected virtual FieldDependencyDescriptor GetDescriptor(IField field)
        {
            return new FieldDependencyDescriptor(field, GetDependencyName(field));
        }
        protected virtual PropertyDependencyDescriptor GetDescriptor(IProperty property)
        {
            return new PropertyDependencyDescriptor(property, GetDependencyName(property));
        }
        protected virtual FactoryDescriptor GetDescriptor(IInvokable methodOrConstructor)
        {
            return new FactoryDescriptor(
                methodOrConstructor,
                methodOrConstructor.GetParameters()
                    .Select(y => new FactoryArgumentDescriptor(y, GetDependencyName(y)))
                    .ToArray());
        }

        protected abstract string GetDependencyName(IField field);
        protected abstract string GetDependencyName(IProperty property);
        protected abstract string GetDependencyName(IParameter argument);

        public abstract IEnumerable<FieldDependencyDescriptor> GetFields(Type type);
        public abstract IEnumerable<PropertyDependencyDescriptor> GetProperties(Type type);
        public abstract IEnumerable<FactoryDescriptor> GetFactories(Type type);
    }
}