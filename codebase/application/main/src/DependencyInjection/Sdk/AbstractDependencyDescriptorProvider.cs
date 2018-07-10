using System;
using System.Collections.Generic;
using System.Linq;

#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.DependencyInjection.Descriptors;
using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    /// <summary>
    /// An abstract class to serve as a base for custom <see cref="IDependencyDescriptorProvider"/> implementations.
    /// </summary>
    /// <seealso cref="IDependencyDescriptorProvider"/>
    public abstract class AbstractDependencyDescriptorProvider : IDependencyDescriptorProvider
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
                    .Select(y => new FactoryArgumentDescriptor(y, GetDependencyName(y)) as IFactoryArgumentDescriptor)
                    .ToArray());
        }

        protected abstract string GetDependencyName(IField field);
        protected abstract string GetDependencyName(IProperty property);
        protected abstract string GetDependencyName(IParameter argument);


        /// <inheritdoc />
        public abstract IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type);

        /// <inheritdoc />
        public abstract IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type);

        /// <inheritdoc />
        public abstract IEnumerable<IFactoryDescriptor> GetFactories(Type type);

        protected bool CompareMemberNames(string left, string right, IEqualityComparer<string> comparer)
        {
            return left.Length > 0 && right.Length > 0
                && char.ToLower(left[0]) == char.ToLower(right[0])
                && comparer.Equals(left.Substring(1), right.Substring(1));
        }

        public virtual bool DoDependenciesConverge(DependencyInfo factoryArgumentDependency, DependencyInfo classMemberDependency)
        {
            factoryArgumentDependency.VerifyArgument(nameof(factoryArgumentDependency)).IsNotNull();
            classMemberDependency.VerifyArgument(nameof(classMemberDependency)).IsNotNull();
            var comparer = StringComparer.Ordinal;

            #if NETSTANDARD || NET45_OR_NEWER
            if (!factoryArgumentDependency.Type.GetTypeInfo().IsAssignableFrom(classMemberDependency.Type.GetTypeInfo()))
            #else
            if (!factoryArgumentDependency.Type.IsAssignableFrom(classMemberDependency.Type))
            #endif
            {
                return false;
            }
            if (factoryArgumentDependency.DependencyName.Length == 0 && classMemberDependency.DependencyName.Length == 0)
            {
                return CompareMemberNames(factoryArgumentDependency.MemberName, classMemberDependency.MemberName, comparer);
            }
            return comparer.Equals(factoryArgumentDependency.DependencyName, classMemberDependency.DependencyName);
        }
    }
}