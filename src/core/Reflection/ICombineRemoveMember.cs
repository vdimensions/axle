namespace Axle.Reflection
{
    public interface ICombineRemoveMember : IMember, IAccessible
    {
        ICombineAccessor CombineAccessor { get; }
        IRemoveAccessor RemoveAccessor { get; }
    }
}