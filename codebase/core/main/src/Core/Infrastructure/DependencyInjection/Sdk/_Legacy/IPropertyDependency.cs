namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
{
    public interface IPropertyDependency : IDependency
    {
        void Set(object target, object value);

        string PropertyName { get; }
    }
}