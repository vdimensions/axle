namespace Axle.Core.DependencyInjection.Descriptors
{
    public interface IFactoryArgumentDescriptor : IDependencyDescriptor
    {
        bool Optional { get; }
        object DefaultValue { get; }
    }
}