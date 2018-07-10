namespace Axle.DependencyInjection.Descriptors
{
    public interface IPropertyDependencyDescriptor : IDependencyDescriptor
    {
        void SetValue(object target, object value);

        object DefaultValue { get; }
    }
}