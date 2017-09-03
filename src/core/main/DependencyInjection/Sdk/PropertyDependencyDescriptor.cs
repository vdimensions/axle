using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class PropertyDependencyDescriptor : IDependencyDescriptor
    {
        public PropertyDependencyDescriptor(IProperty property, string dependencyName)
        {
            Member = property.VerifyArgument(nameof(property)).IsNotNull().Value;
            Info = new DependencyInfo(
                property.MemberType,
                dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull(), 
                property.Name);
        }

        public IProperty Member { get; }
        public DependencyInfo Info { get; }
        public bool Optional { get; }
    }
}