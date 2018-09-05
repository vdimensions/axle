#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.ComponentModel
{
    public interface IComponentDriver
    {
        object Resolve();
    }
    public interface IComponentDriver<T> : IComponentDriver
    {
        T Resolve();
    }
}
#endif
