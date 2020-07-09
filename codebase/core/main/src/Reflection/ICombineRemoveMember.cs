namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing a class member of a delegate type, that
    /// supports adding and removing delegates.
    /// Typically, this is a representation event handlers.
    /// </summary>
    public interface ICombineRemoveMember : ICombinableMember, IRemovableMember
    {
    }

    /// <summary>
    /// An interface representing a class member of a delegate type, that
    /// supports delegate additivity.
    /// </summary>
    public interface ICombinableMember : IMember, IAccessible
    {
        /// <summary>
        /// Gets a reference to the <see cref="ICombineAccessor"/> which enables delegate additivity.
        /// </summary>
        ICombineAccessor CombineAccessor { get; }
    }

    /// <summary>
    /// An interface representing a class member of a delegate type, that
    /// supports delegate subtraction.
    /// </summary>
    public interface IRemovableMember : IMember, IAccessible
    {
        /// <summary>
        /// Gets a reference to the <see cref="IRemoveAccessor"/> which enables delegate subtraction.
        /// </summary>
        IRemoveAccessor RemoveAccessor { get; }
    }
}