using System;


namespace Axle.Threading
{
    /// <summary>
    /// An interface representing a synchronization lock construct.
    /// </summary>
    public interface ILock
    {
        /// <summary>
        /// Acquires an exclusive lock.
        /// </summary>
        void Enter();

        /// <summary>
        /// Releases a previously obtained exclusive lock.
        /// </summary>
        void Exit();

        /// <summary>
        /// Attempts to acquire an exclusive lock.
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait for the lock.
        /// A value of <c>-1</c> represents an infinite wait.
        /// </param>
        /// <seealso cref="System.Threading.Timeout.Infinite"/>
        bool TryEnter(int millisecondsTimeout);

        /// <summary>
        /// Attempts to acquire an exclusive lock.
        /// </summary>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> representing the amount of time to wait for the lock.
        /// </param>
        bool TryEnter(TimeSpan timeout);
    }
}