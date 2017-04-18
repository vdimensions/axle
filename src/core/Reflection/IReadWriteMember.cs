namespace Axle.Reflection
{
    public interface IReadWriteMember : IMember, IAccessible
    {
        IGetAccessor GetAccessor { get; }
        ISetAccessor SetAccessor { get; }
    }
}
