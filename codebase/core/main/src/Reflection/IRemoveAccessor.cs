using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing the acessor that allows removing a delegate member from a delegate invocation list.
    /// Usually, this repreents removing an event handler from an event member.
    /// </summary>
    /// <seealso cref="IRemovableMember"/>
    /// <seealso cref="ICombineAccessor"/>
    /// <seealso cref="IAccessor"/>
    public interface IRemoveAccessor : IAccessor
    {
        void RemoveDelegate(object target, Delegate handler);
    }
}