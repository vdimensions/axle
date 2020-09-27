using System;


namespace Axle.Reflection
{
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