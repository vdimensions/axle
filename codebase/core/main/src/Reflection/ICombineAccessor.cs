using System;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface representing the acessor that allows adding a delegate to a delegate invocation list.
    /// Usually, this repreents assigning an event handler to an event member.
    /// </summary>
    /// <seealso cref="ICombinableMember"/>
    /// <seealso cref="IRemoveAccessor"/>
    /// <seealso cref="IAccessor"/>
    public interface ICombineAccessor : IAccessor
    {
        /// <summary>
        /// Combines the target delegate with the one provided by the <paramref name="handler"/>.
        /// </summary>
        /// <param name="target">
        /// The object instance that declares the <see cref="ICombinableMember"/> we are appending a
        /// <paramref name="handler"/> to.
        /// </param>
        /// <param name="handler">
        /// The delegate to combine.
        /// </param>
        void AddDelegate(object target, Delegate handler);
    }
}