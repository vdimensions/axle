namespace Axle.Core.Infrastructure.DependencyInjection.Descriptors
{
    public interface IPropertyDependencyDescriptor : IDependencyDescriptor
    {
        void SetValue(object target, object value);
    }
}