using System;
using System.Reflection;

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

            if (!factoryArgumentDependency.Type.GetTypeInfo().IsAssignableFrom(classMemberDependency.Type.GetTypeInfo()))
            {
                return false;
            }
            if (factoryArgumentDependency.DependencyName.Length == 0 && classMemberDependency.DependencyName.Length == 0)
            {
                //return char.ToLower(factoryArgumentDependency.MemberName[0]) == char.ToLower(classMemberDependency.MemberName[0]) &&
                //       comparer.Equals(factoryArgumentDependency.MemberName.Substring(1), classMemberDependency.MemberName.Substring(1));
                return CompareMemberNames(factoryArgumentDependency.MemberName, classMemberDependency.MemberName, comparer);
            }
            return comparer.Equals(factoryArgumentDependency.DependencyName, classMemberDependency.DependencyName);
        }
    }
}