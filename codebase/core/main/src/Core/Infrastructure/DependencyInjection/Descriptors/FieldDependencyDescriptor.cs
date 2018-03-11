using Axle.Reflection;
using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public class FieldDependencyDescriptor : IPropertyDependencyDescriptor
    {
        public FieldDependencyDescriptor(IField field, string dependencyName)
        {
            Member = field.VerifyArgument(nameof(field)).IsNotNull().Value;
            Info = new DependencyInfo(
                field.MemberType,
                dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull(),
                field.Name);
        }

        void IPropertyDependencyDescriptor.SetValue(object target, object value)
        {
            Member.SetAccessor.SetValue(target, value);
        }

        public IField Member { get; }
        public DependencyInfo Info { get; }
    }
}