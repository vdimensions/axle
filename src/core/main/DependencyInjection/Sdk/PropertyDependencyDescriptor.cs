using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class PropertyDependencyDescriptor : IDependencyDescriptor
    {
        public PropertyDependencyDescriptor(DependencyInfo info, bool optional)
        {
            Info = info.VerifyArgument(nameof(info)).IsNotNull();
            Optional = optional;
        }

        public DependencyInfo Info { get; }
        public bool Optional { get; }
    }
}