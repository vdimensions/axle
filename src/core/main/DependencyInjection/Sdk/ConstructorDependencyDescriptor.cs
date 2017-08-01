using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class ConstructorDependencyDescriptor : IDependencyDescriptor
    {
        public ConstructorDependencyDescriptor(DependencyInfo info, object defaultValue)
        {
            Info = info.VerifyArgument(nameof(info)).IsNotNull();
            DefaultValue = defaultValue;
        }

        public DependencyInfo Info { get; }
        public object DefaultValue { get; }
    }
}