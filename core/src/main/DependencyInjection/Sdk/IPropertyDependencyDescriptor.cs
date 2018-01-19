namespace Axle.DependencyInjection.Sdk
{
    public interface IPropertyDependencyDescriptor : IDependencyDescriptor
    {
        void SetValue(object target, object value);
    }
}