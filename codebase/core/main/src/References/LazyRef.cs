#if NETSTANDARD || NET40_OR_NEWER
using System;
using System.Threading;

using Axle.Verification;


namespace Axle.References
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class LazyRef<T> : ILazyReference<T>
    {
        private readonly Lazy<T> _lazy;

        private LazyRef(Lazy<T> t) => _lazy = t;
        public LazyRef(Func<T> valueFactory, LazyThreadSafetyMode mode)
            : this(new Lazy<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value, mode)) { }
        public LazyRef(Func<T> valueFactory)
            : this(new Lazy<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value)) { }

        internal LazyRef(){}

        /// <summary>
        /// Tries to retrieve the value that is referenced by the current <see cref="ILazyReference{T}"/> object.
        /// <remarks>
        /// Calling this method will not enforce the lazy value initialization.
        /// </remarks>
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the reference value, if it is available.
        /// This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the target value was retrieved; <c>false</c> otherwise.
        /// </returns>
        public bool TryGetValue(out T value)
        {
            if (_lazy.IsValueCreated)
            {
                value = _lazy.Value;
                return true;
            }
            value = default(T);
            return false;
        }

        public T Value => _lazy.Value;

        /// <summary>
        /// Gets a boolean value that indicates whether the current
        /// <see cref="ILazyReference{T}"/> has a <see cref="Value"/> created.
        /// </summary>
        public bool HasValue => _lazy.IsValueCreated;

        T IReference<T>.Value => Value;
        object IReference.Value => Value;
    }
}
#endif