#if NETSTANDARD || NET35_OR_NEWER
using System;


namespace Axle
{
    public partial class Signal
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="dereference">
        /// A function that dereferences the signal's underlying delegate for the signal's internal processing. 
        /// </param>
        /// <param name="update">
        /// A function that updates the underlying signal reference. Used for the signal's internal processing. 
        /// </param>
        public static Signal Create(Func<Action> dereference, Func<Action, Action> update)
        {
            return new Signal(dereference, update);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="dereference">
        /// A function that dereferences the signal's underlying delegate for the signal's internal processing. 
        /// </param>
        /// <param name="update">
        /// A function that updates the underlying signal reference. Used for the signal's internal processing. 
        /// </param>
        public static Signal<T> Create<T>(Func<Action<T>> dereference, Func<Action<T>, Action<T>> update)
        {
            return new Signal<T>(dereference, update);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="dereference">
        /// A function that dereferences the signal's underlying delegate for the signal's internal processing. 
        /// </param>
        /// <param name="update">
        /// A function that updates the underlying signal reference. Used for the signal's internal processing. 
        /// </param>
        public static Signal<T1, T2> Create<T1, T2>(Func<Action<T1, T2>> dereference, Func<Action<T1, T2>, Action<T1, T2>> update)
        {
            return new Signal<T1, T2>(dereference, update);
        }
    }
}
#endif