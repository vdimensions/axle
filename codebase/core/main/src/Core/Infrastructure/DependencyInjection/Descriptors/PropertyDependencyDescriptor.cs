using Axle.Reflection;
using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public class PropertyDependencyDescriptor : IPropertyDependencyDescriptor
    {
        public PropertyDependencyDescriptor(IProperty property, string dependencyName)
        {
            Member = property.VerifyArgument(nameof(property)).IsNotNull().Value;
            Info = new DependencyInfo(
                property.MemberType,
                dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull(), 
                property.Name);
        }

        public void SetValue(object target, object value)
        {
            Member.SetAccessor.SetValue(target, value);
        }

        public IProperty Member { get; }
        public DependencyInfo Info { get; }
    }
}