#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Reflection
{
    public interface IReadWriteMember : IReadableMember, IWriteableMember
    {
    }
}
#endif
