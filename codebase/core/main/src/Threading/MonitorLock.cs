#if NETSTANDARD || NET20_OR_NEWER || (UNITY_2018_1_OR_NEWER && (UNITY_EDITOR || !UNITY_WEBGL))
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Axle.Threading
{
    /// <summary>
    /// A monitor lock class. Uses the regular <see cref="Monitor"/> API for issuing locks on a provided 
    /// <see cref="object"/>.
    /// </summary>
    public class MonitorLock : ILock
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MonitorLock"/> class.
        /// </summary>
        /// <returns>
        /// A <see cref="MonitorLock"/> instance.
        /// </returns>
        public static MonitorLock Create() => new MonitorLock();
        
        private readonly object _obj;

        /// <summary>
        /// Creates a new instance of the <see cref="MonitorLock"/> class.
        /// </summary>
        /// <param name="obj">
        /// An <see cref="object"/> instance to issue the locks upon.
        /// </param>
        public MonitorLock(object obj) 
        {
            _obj = obj ?? throw new ArgumentNullException(nameof(obj));
        }
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

        /// <summary>
        /// Creates a <see cref="IDisposable">disposable</see> <see cref="MonitorLockHandle"/> object. As a result,
        /// the current <see cref="ILock">lock</see> instance will acquire and keep a lock until the returned
        /// handle is disposed of.
        /// </summary>
        /// <returns>
        /// A <see cref="MonitorLockHandle"/> object.
        /// </returns>
        /// <seealso cref="MonitorLockHandle"/>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public MonitorLockHandle CreateHandle() => new MonitorLockHandle(this);
        ILockHandle ILock.CreateHandle() => CreateHandle();

        internal bool Wait(TimeSpan timeout) => Monitor.Wait(_obj, timeout);
        internal bool Wait() => Monitor.Wait(_obj);
        internal void Pulse() => Monitor.Pulse(_obj);
        internal void PulseAll() => Monitor.PulseAll(_obj);

        #if NETSTANDARD || NET45_OR_NEWER
        /// <summary>
        /// Determines whether the current thread holds the current <see cref="MonitorLock"/>.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public bool IsLocked => Monitor.IsEntered(_obj);
        #endif
    }
}
#endif