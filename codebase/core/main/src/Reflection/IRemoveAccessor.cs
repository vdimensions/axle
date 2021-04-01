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
        /// <summary>
        /// Removes the specified <paramref name="handler"/> from the invocation list of the represented delegate member
        /// or event handler.
        /// </summary>
        /// <param name="target">
        /// The object instance that declares the delegate or event handler method to remove the
        /// <paramref name="handler"/> from.
        /// </param>
        /// <param name="handler">
        /// The <see cref="Delegate"/> instance to be removed from the invocation list of the represented delegate
        /// member or event handler.
        /// </param>
        void RemoveDelegate(object target, Delegate handler);
    }
}