namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a class member of a delegate type, that
    /// supports adding and removing delegates.
    /// Usually, this an event handler qualifies for this definition.
    /// </summary>
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