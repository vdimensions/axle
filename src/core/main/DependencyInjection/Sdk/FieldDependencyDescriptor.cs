using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class FieldDependencyDescriptor : IDependencyDescriptor
    {
        public FieldDependencyDescriptor(IField field, string dependencyName)
        {
            Member = field.VerifyArgument(nameof(field)).IsNotNull().Value;
            Info = new DependencyInfo(
                field.MemberType,
                dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull(),
                field.Name);
        }

        public IField Member { get; }
        public DependencyInfo Info { get; }
        public bool Optional { get; }
    }
}