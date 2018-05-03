namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public interface IFactoryArgumentDescriptor : IDependencyDescriptor
    {
        bool Optional { get; }
        object DefaultValue { get; }
    }
}