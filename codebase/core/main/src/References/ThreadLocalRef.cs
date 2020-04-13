#if NETSTANDARD || NET40_OR_NEWER
using System;
using System.Threading;

using Axle.Verification;


namespace Axle.References
{
    /// <summary>
    /// Provides thread-local storage of data.
    /// </summary>
    /// <typeparam name="T">
    /// Specifies the type of data this <see cref="ThreadLocalRef{T}"/> instance stores per-thread.
    /// </typeparam>
    public sealed class ThreadLocalRef<T> : IThreadLocalReference<T>, IDisposable
    {
        private readonly ThreadLocal<T> _t;

        private ThreadLocalRef(ThreadLocal<T> t) => _t = t;
        /// <summary>
        /// Initializes the <see cref="ThreadLocalRef{T}"/> instance.
        /// </summary>
        public ThreadLocalRef() : this(new ThreadLocal<T>()) { }
        /// <summary>
        /// Initializes the <see cref="ThreadLocalRef{T}"/> instance with the specified <paramref name="valueFactory"/>
        /// function.
        /// </summary>
        /// <param name="valueFactory">
        /// The <see cref="T:System.Func{T}"/> invoked to produce a lazily-initialized value when 
        /// an attempt is made to retrieve <see cref="Value"/> without it having been previously initialized.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="valueFactory"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic).
        /// </exception>
        public ThreadLocalRef(Func<T> valueFactory)
            : this(new ThreadLocal<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value)) { }

        /// <summary>
        /// Gets or sets the value of this <see cref="ThreadLocalRef{T}"/> instance for the current thread.
        /// </summary>
        /// <returns>
        /// Returns an instance of the object that this <see cref="ThreadLocalRef{T}"/> is responsible for initializing.
        /// </returns>
        public T Value
        {
            get => _t.Value;
            set => _t.Value = value;
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="Value"/> has been initialized for the current thread.
        /// </summary>
        public bool HasValue => _t.IsValueCreated;

        /// <summary>
        /// Releases all resources used by the current <see cref="ThreadLocalRef{T}"/> instance.
        /// </summary>
        public void Dispose() => _t?.Dispose();

        bool IReference<T>.TryGetValue(out T value)
        {
            if (_t.IsValueCreated)
            {
                value = _t.Value;
                return true;
            }
            value = default(T);
            return false;
        }
        T IReference<T>.Value => Value;
        
        object IReference.Value => Value;

        void IDisposable.Dispose() => Dispose();
    }
}
#endif