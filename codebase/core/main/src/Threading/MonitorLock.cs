using System;
using System.Threading;


namespace Axle.Threading
{
    /// <summary>
    /// A monitor lock class. Uses the regular <see cref="Monitor"/> API for issuing locks on a provided <see cref="object"/>.
    /// </summary>
    public class MonitorLock : ILock
    {
        private readonly object _obj;

        /// <summary>
        /// Creates a new instance of the <see cref="MonitorLock"/> class.
        /// </summary>
        /// <param name="obj">
        /// A <see cref="object"/> instance to issue the locks upon.
        /// </param>
        public MonitorLock(object obj) => _obj = obj;
        /// <summary>
        /// Creates a new instance of the <see cref="MonitorLock"/> class.
        /// </summary>
        public MonitorLock() : this(new object()) { }

        /// <inheritdoc />
        public void Enter() => Monitor.Enter(_obj);

        /// <inheritdoc />
        public void Exit() => Monitor.Exit(_obj);

        /// <inheritdoc />
        public bool TryEnter(int millisecondsTimeout) => Monitor.TryEnter(_obj, millisecondsTimeout);

        /// <inheritdoc />
        public bool TryEnter(TimeSpan timeout) => Monitor.TryEnter(_obj, timeout);
    }
}