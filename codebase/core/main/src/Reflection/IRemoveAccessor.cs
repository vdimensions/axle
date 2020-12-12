using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing the accessor that allows removing a delegate member from a delegate invocation list.
    /// Usually, this represents removing an event handler from an event member.
    /// </summary>
    /// <seealso cref="IRemovableMember"/>
    /// <seealso cref="ICombineAccessor"/>
    /// <seealso cref="IAccessor"/>
    public interface IRemoveAccessor : IAccessor
    {
        void RemoveDelegate(object target, Delegate handler);
    }
}