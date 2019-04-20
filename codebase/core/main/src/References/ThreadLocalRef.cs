#if NETSTANDARD || NET40_OR_NEWER
using System;
using System.Threading;

using Axle.Verification;


namespace Axle.References
{
    public sealed class ThreadLocalRef<T> : IThreadLocalReference<T>, IDisposable
    {
        private readonly ThreadLocal<T> _t;

        private ThreadLocalRef(ThreadLocal<T> t) => _t = t;
        public ThreadLocalRef()
            : this(new ThreadLocal<T>()) { }
        public ThreadLocalRef(Func<T> valueFactory)
            : this(new ThreadLocal<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value)) { }

        public T Value
        {
            get => _t.Value;
            set => _t.Value = value;
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="Value"/> has been initialized for the current thread.
        /// </summary>
        public bool HasValue => _t.IsValueCreated;

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