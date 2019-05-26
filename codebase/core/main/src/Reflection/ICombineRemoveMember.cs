namespace Axle.Reflection
{
    public interface ICombineRemoveMember : ICombinableMember, IRemoveableMember
    {
    }

    public interface ICombinableMember : IMember, IAccessible
    {
        ICombineAccessor CombineAccessor { get; }
    }

    public interface IRemoveableMember : IMember, IAccessible
    {
        IRemoveAccessor RemoveAccessor { get; }
    }
}