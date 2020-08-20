#if NETSTANDARD || NET40_OR_NEWER
using System;
using System.Collections.Generic;
using System.Threading;
using Axle.Verification;


namespace Axle.References
{
    /// <summary>
    /// The default implementation of the <see cref="ILazyReference{T}"/> interface. This class acts as a wrapper to 
    /// <see cref="Lazy{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object that is being lazily initialized.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public sealed class LazyRef<T> : ILazyReference<T>, IEquatable<LazyRef<T>>
    {
        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> implementation that can compare lazy references.
        /// Two lazy references are deemed equal in when both are not initialized or if their values are considered
        /// equal by the <see cref="P:LazyRef{T}.EqualityComparer.ValueComparer"/>. 
        /// </summary>
        /// <seealso cref="IEqualityComparer{T}"/>
        /// <seealso cref="WeakRef{T}"/>
        /// <seealso cref="IWeakReference{T}"/>
        public sealed class EqualityComparer : AbstractReferenceEqualityComparer<T, ILazyReference<T>>
        {
            /// <summary>
            /// Creates a new instance of the <see cref="EqualityComparer"/> class that uses
            /// the <see cref="EqualityComparer{T}.Default">default comparer</see> for comparing the reference value.
            /// </summary>
            /// <returns>
            /// A new instance of the <see cref="EqualityComparer"/> class.
            /// </returns>
            public static EqualityComparer Create() => new EqualityComparer();
            /// <summary>
            /// Creates a new instance of the <see cref="EqualityComparer"/> class that uses the supplied
            /// <paramref name="valueComparer"/> for comparing the reference value.
            /// </summary>
            /// <returns>
            /// A new instance of the <see cref="EqualityComparer"/> class.
            /// </returns>
            public static EqualityComparer Create(IEqualityComparer<T> valueComparer)
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(valueComparer, nameof(valueComparer)));
                return new EqualityComparer(valueComparer);
            }

            private EqualityComparer() { }
            private EqualityComparer(IEqualityComparer<T> valueComparer) : base(valueComparer) { }
        }
        
        private readonly Lazy<T> _lazy;

        private LazyRef(Lazy<T> t) => _lazy = t;
        /// <summary>
        /// Creates a new instance of the <see cref="LazyRef{T}"/> class using the provided
        /// <paramref name="valueFactory"/> and thread-safety <paramref name="mode"/>.
        /// </summary>
        /// <param name="valueFactory">
        /// The delegate that is used to produce the lazily-initialized value when needed
        /// </param>
        /// <param name="mode">
        /// One of the <see cref="LazyThreadSafetyMode"/> enumeration values that specifies the thread-safety mode. 
        /// </param>
        public LazyRef(Func<T> valueFactory, LazyThreadSafetyMode mode)
            : this(new Lazy<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value, mode)) { }
        /// <summary>
        /// Creates a new instance of the <see cref="LazyRef{T}"/> class using the provided
        /// <paramref name="valueFactory"/>.
        /// </summary>
        /// <param name="valueFactory">
        /// The delegate that is used to produce the lazily-initialized value when needed
        /// </param>
        public LazyRef(Func<T> valueFactory)
            : this(new Lazy<T>(valueFactory.VerifyArgument(nameof(valueFactory)).IsNotNull().Value)) { }
        /// This constructor is used for deserialization
        internal LazyRef() { }
        
        /// <inheritdoc />
        public override bool Equals(object other)
        {
            switch (other)
            {
                case null when !HasValue:
                    return true;
                case T otherVal:
                    return TryGetValue(out var val) && Equals(val, otherVal);
                case LazyRef<T> otherLazyRef:
                    return EqualityComparer.Create().Equals(this, otherLazyRef);
                case ILazyReference<T> otherLazy:
                    return EqualityComparer.Create().Equals(this, otherLazy);
                default:
                    return false;
            }
        }

        bool IEquatable<ILazyReference<T>>.Equals(ILazyReference<T> other) 
            => EqualityComparer.Create().Equals(this, other);

        bool IEquatable<LazyRef<T>>.Equals(LazyRef<T> other) 
            => EqualityComparer.Create().Equals(this, other);
        
        bool IEquatable<T>.Equals(T other) 
            => TryGetValue(out var v) && EqualityComparer.Create().ValueComparer.Equals(v, other);

        /// <inheritdoc />
        public override int GetHashCode() => TryGetValue(out var val) ? val.GetHashCode() : 0;

        /// <summary>
        /// Tries to retrieve the value that is referenced by the current <see cref="LazyRef{T}"/> object.
        /// <remarks>
        /// Calling this method will not enforce the lazy value initialization.
        /// </remarks>
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the value that has been created for the current <see cref="LazyRef{T}"/>
        /// instance, if it is available.
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

        /// <summary>
        /// Gets the lazily initialized value of the current <see cref="LazyRef{T}"/> instance.
        /// </summary>
        public T Value => _lazy.Value;

        /// <summary>
        /// Gets a boolean value that indicates whether a value has been created for the current
        /// <see cref="LazyRef{T}"/> instance.
        /// </summary>
        public bool HasValue => _lazy.IsValueCreated;

        T IReference<T>.Value => Value;
        object IReference.Value => Value;
    }
}
#endif