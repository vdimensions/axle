#if NETSTANDARD1_3_OR_NEWER || NET35_OR_NEWER 
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Threading
{
    /// <summary>
    /// A lock provider that is capable of associating a particular lock object with a key.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TLock"></typeparam>
    public class KeyedLockProvider<T, TLock> where TLock: ILock
    {
        private readonly ConcurrentDictionary<T, TLock> _locks;
        private readonly Func<TLock> _lockFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedLockProvider{T,TLock}"/> class using the supplied
        /// <paramref name="lockFactory"/> function and <paramref name="comparer"/>.
        /// </summary>
        /// <param name="lockFactory">
        /// A function that is used to create an individual <typeparamref name="TLock" /> instance for each key,
        /// </param>
        /// <param name="comparer">
        /// An <see cref="IEqualityComparer{T}"/> instance that is used to distinguish among the lock keys.
        /// </param>
        public KeyedLockProvider(Func<TLock> lockFactory, IEqualityComparer<T> comparer)
        {
            _lockFactory = lockFactory;
            _locks = new ConcurrentDictionary<T, TLock>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedLockProvider{T,TLock}"/> class using the supplied
        /// <paramref name="lockFactory"/> function and a
        /// <see cref="EqualityComparer{T}.Default">default</see> key comparer.
        /// </summary>
        /// <param name="lockFactory">
        /// A function that is used to create an individual <typeparamref name="TLock" /> instance for each key,
        /// </param>
        public KeyedLockProvider(Func<TLock> lockFactory) : this(lockFactory, EqualityComparer<T>.Default) { }

        /// <summary>
        /// Attempts to remove and return the lock that has the specified <paramref name="key"/>
        /// from the <see cref="KeyedLockProvider{T,TLock}"/>.
        /// </summary>
        /// <param name="key">
        /// The key of the lock to remove and return.
        /// </param>
        /// <param name="removedLock">
        /// When this method returns, contains the lock removed from the <see cref="KeyedLockProvider{T,TLock}"/>, or
        /// <c>null</c> type if <paramref name="key"/> does not exist.
        /// </param>
        /// <returns>
        /// <c>true</c> if the object was removed successfully; otherwise, <c>false</c>.
        /// </returns>
        public bool TryRemove(T key, out TLock removedLock)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key)));
            lock (_locks)
            {
                return _locks.TryRemove(key, out removedLock);
            }
        }
        /// <summary>
        /// Attempts to remove and return the lock that has the specified <paramref name="key"/>
        /// from the <see cref="KeyedLockProvider{T,TLock}"/>.
        /// </summary>
        /// <param name="key">
        /// The key of the lock to remove and return.
        /// </param>
        /// <returns>
        /// <c>true</c> if the object was removed successfully; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="TryRemove(T,out TLock)"/>
        public bool TryRemove(T key) => TryRemove(key, out _);

        /// <summary>
        /// Gets the lock object that is associated with the provided <paramref name="key"/>, or creates a new lock
        /// instance in case none exists.
        /// </summary>
        /// <param name="key"></param>
        public TLock this[T key]
        {
            get
            {
                //
                // Why the lock statement you may ask?
                // 
                // Long story short -- the `GetOrAdd` method of a concurrent dictionary is not thread-safe in a way that
                // the provided value factory can be invoked more than once for the same key, in case multiple threads
                // make the call simultaneously. In our scenario we call our `_lockFactory` function which must be
                // called only once per-key, otherwise we may end up providing different lock objects for the same key.
                // Well, the bugs that could follow wouldn't be fun investigating...
                // 
                // More info can be found in this stackoverflow post:
                // https://stackoverflow.com/questions/10486579/concurrentdictionary-pitfall-are-delegates-factories-from-getoradd-and-addorup
                //
                
                //
                // Given the above assumptions, we will try to avoid the lock in case our dictionary contains an item
                // already. This should be safe, since the only place we add locks is this method.
                //
                // ReSharper disable once InconsistentlySynchronizedField
                if (!_locks.TryGetValue(key, out var l))
                {
                    lock (_locks)
                    {
                        #if NETSTANDARD2_1_OR_NEWER
                        return _locks.GetOrAdd(key, (_, lockFactory) => lockFactory(), _lockFactory);
                        #else
                        return _locks.GetOrAdd(key, _ => _lockFactory());
                        #endif
                    }
                }

                return l;
            }
        }
    }
}
#endif