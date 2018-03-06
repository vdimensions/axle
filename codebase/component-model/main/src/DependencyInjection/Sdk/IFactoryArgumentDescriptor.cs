namespace Axle.DependencyInjection.Sdk
{
    public interface IFactoryArgumentDescriptor : IDependencyDescriptor
    {
        bool Optional { get; }
        object DefaultValue { get; }
    }
}