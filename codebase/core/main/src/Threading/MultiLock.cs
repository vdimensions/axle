using System;

namespace Axle.Threading
{
    /// <summary>
    /// An implementation of the <see cref="ILock"/> interface that acts as a wrapper around a set of
    /// supplied <see cref="ILock"/> instances.
    /// The <see cref="MultiLock"/> lock inhibits a "locked" state only if all underlying locks have acquired the lock
    /// state themselves.
    /// </summary>
    public sealed class MultiLock : ILock
    {
        private readonly ILock[] _locks;

        /// <summary>
        /// Creates a new instance of the <see cref="MultiLock"/> class.
        /// </summary>
        /// <param name="locks">
        /// A collection of <see cref="ILock"/> implementations to be switched together.
        /// </param>
        public MultiLock(params ILock[] locks)
        {
            _locks = locks;
        }

        /// <inheritdoc />
        public ILockHandle CreateHandle() => new GenericLockHandle(this);

        /// <summary>
        /// Acquires an exclusive lock for each of the underlying lock objects in the they were provided in
        /// the constructor.
        /// </summary>
        public void Enter()
        {
            for (var i = 0; i < _locks.Length; ++i)
            {
                _locks[i].Enter();
            }
        }

        /// <summary>
        /// Releases previously acquired lock for each of the underlying lock objects in reverse of the lock
        /// acquisition order.
        /// </summary>
        public void Exit()
        {
            for (var i = _locks.Length - 1; i >= 0; i--)
            {
                _locks[i].Exit();
            }
        }

        /// <summary>
        /// Attempts to acquire an exclusive lock on all underlying lock objects in the order they were provided in the
        /// constructor.
        /// <remarks>
        /// If at lease one of the underlying locks objects fails to acquire a lock, all successfully
        /// acquired locks from this call will be released in reverse of the acquisition order.
        /// </remarks> 
        /// </summary>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait for the lock. A value of <c>-1</c> represents an infinite wait.
        /// </param>
        /// <returns>
        /// <c>true</c> if the lock was acquired successfully; <c>false</c> otherwise.
        /// </returns>
        public bool TryEnter(int millisecondsTimeout)
        {
            var i = 0;
            for (; i < _locks.Length;)
            {
                if (_locks[i].TryEnter(millisecondsTimeout))
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (i < _locks.Length)
            {
                for (var j = i - 1; j >= 0; j--)
                {
                    _locks[j].Exit();
                }
            }

            return true;
        }

        /// <summary>
        /// Attempts to acquire an exclusive lock on all underlying lock objects in the order they were provided in the
        /// constructor.
        /// <remarks>
        /// If at lease one of the underlying locks objects fails to acquire a lock, all successfully
        /// acquired locks from this call will be released in reverse of the acquisition order.
        /// </remarks> 
        /// </summary>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> representing the amount of time to wait for the lock.
        /// </param>
        /// <returns>
        /// <c>true</c> if the lock was acquired successfully; <c>false</c> otherwise.
        /// </returns>
        public bool TryEnter(TimeSpan timeout)
        {
            var i = 0;
            for (; i < _locks.Length;)
            {
                if (_locks[i].TryEnter(timeout))
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            if (i < _locks.Length)
            {
                for (var j = i - 1; j >= 0; j--)
                {
                    _locks[j].Exit();
                }
            }

            return true;
        }
    }
}