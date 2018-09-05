#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.References
{
    public interface ISingletonReference : IReference { }

    public interface ISingetonReference<T> : IReference<T>, ISingletonReference { }
}
#endif