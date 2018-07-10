#if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
namespace Axle.DependencyInjection
{
    public sealed class DefaultDependencyContainerProvider : IDependencyContainerProvider
    {
        public IContainer Create(IContainer parent) => new Container(parent);

        public IContainer Create() => new Container(null);
    }
}
#endif