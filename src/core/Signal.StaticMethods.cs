using System;

namespace Axle
{
    public partial class Signal
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="action">
        /// The underlying action to represent the created signal instance. 
        /// </param>
        public static Signal Create(Action action)
        {
            return new Signal(() => action, x => action = x);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="action">
        /// The underlying action to represent the created signal instance. 
        /// </param>
        public static Signal<T> Create<T>(Action<T> action)
        {
            return new Signal<T>(() => action, x => action = x);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Signal"/> class using the specified action object.
        /// </summary>
        /// <param name="action">
        /// The underlying action to represent the created signal instance. 
        /// </param>
        public static Signal<T1, T2> Create<T1, T2>(Action<T1, T2> action)
        {
            return new Signal<T1, T2>(() => action, x => action = x);
        }
    }
}

