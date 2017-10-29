using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Reflection;


namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractDependencyDescriptorProvider : IDependencyDescriptorProvider
    {
        protected virtual IPropertyDependencyDescriptor GetDescriptor(IField field)
        {
            return new FieldDependencyDescriptor(field, GetDependencyName(field));
        }
        protected virtual IPropertyDependencyDescriptor GetDescriptor(IProperty property)
        {
            return new PropertyDependencyDescriptor(property, GetDependencyName(property));
        }
        protected virtual IFactoryDescriptor GetDescriptor(IInvokable methodOrConstructor)
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

        public abstract IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type);
        public abstract IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type);
        public abstract IEnumerable<IFactoryDescriptor> GetFactories(Type type);

        protected bool CompareMemberNames(string left, string right, IEqualityComparer<string> comparer)
        {
            return left.Length > 0 && right.Length > 0
                && char.ToLower(left[0]) == char.ToLower(right[0])
                && comparer.Equals(left.Substring(1), right.Substring(1));
        }
    }
}