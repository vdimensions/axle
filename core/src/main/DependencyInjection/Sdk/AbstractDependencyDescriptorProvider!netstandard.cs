using System;

using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyDescriptorProvider
    {
        public virtual bool DoDependenciesConverge(
            DependencyInfo factoryArgumentDependency, 
            DependencyInfo classMemberDependency)
        {
            factoryArgumentDependency.VerifyArgument(nameof(factoryArgumentDependency)).IsNotNull();
            classMemberDependency.VerifyArgument(nameof(classMemberDependency)).IsNotNull();
            var comparer = StringComparer.Ordinal;

            if (!factoryArgumentDependency.Type.IsAssignableFrom(classMemberDependency.Type))
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