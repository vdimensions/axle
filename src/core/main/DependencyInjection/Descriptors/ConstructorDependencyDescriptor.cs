using Axle.Verification;

namespace Axle.DependencyInjection.Descriptors
{
    public class ConstructorDependencyDescriptor
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