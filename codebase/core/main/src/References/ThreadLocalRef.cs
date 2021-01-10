#if NETSTANDARD || NET40_OR_NEWER || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
using System;
using System.Collections.Generic;
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
    public sealed class ThreadLocalRef<T> : IThreadLocalReference<T>, IEquatable<ThreadLocalRef<T>>, IDisposable
    {
        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> implementation that can compare thread-local references.
        /// Two thread-local references are deemed equal in when both are not initialized or if their values are
        /// considered equal by the <see cref="P:ThreadLocalRef{T}.EqualityComparer.ValueComparer"/>. 
        /// </summary>
        /// <seealso cref="IEqualityComparer{T}"/>
        /// <seealso cref="WeakRef{T}"/>
        /// <seealso cref="IWeakReference{T}"/>
        public sealed class EqualityComparer : AbstractReferenceEqualityComparer<T, IThreadLocalReference<T>>
        {
            /// <summary>
            /// Creates a new instance of the <see cref="EqualityComparer"/> class that uses
            /// the <see cref="ReferenceEqualityComparer{T}">default comparer</see> for comparing the reference value.
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
        
        /// <inheritdoc />
        public override bool Equals(object other)
        {
            switch (other)
            {
                case null when !HasValue:
                    return true;
                case T otherVal:
                    return TryGetValue(out var val) && Equals(val, otherVal);
                case ThreadLocalRef<T> otherRef:
                    return EqualityComparer.Create().Equals(this, otherRef);
                case IThreadLocalReference<T> otherReference:
                    return EqualityComparer.Create().Equals(this, otherReference);
                default:
                    return false;
            }
        }

        bool IEquatable<IThreadLocalReference<T>>.Equals(IThreadLocalReference<T> other) 
            => EqualityComparer.Create().Equals(this, other);

        bool IEquatable<ThreadLocalRef<T>>.Equals(ThreadLocalRef<T> other) 
            => EqualityComparer.Create().Equals(this, other);
        
        bool IEquatable<T>.Equals(T other) 
            => TryGetValue(out var v) && EqualityComparer.Create().ValueComparer.Equals(v, other);

        /// <inheritdoc />
        public override int GetHashCode() => EqualityComparer.Create().GetHashCode(this);
        
        /// <summary>
        /// Tries to retrieve the value that is referenced by the current <see cref="ThreadLocalRef{T}"/> instance.
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the value that has been assigned to the current
        /// <see cref="ThreadLocalRef{T}"/> instance, if it is available.
        /// This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the target value was retrieved; <c>false</c> otherwise.
        /// </returns>
        public bool TryGetValue(out T value)
        {
            if (_t.IsValueCreated)
            {
                value = _t.Value;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>
        /// Gets or sets the value of this <see cref="ThreadLocalRef{T}"/> instance for the current thread.
        /// </summary>
        /// <returns>
        /// Returns the value of this <see cref="ThreadLocalRef{T}"/> instance for the current thread.
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

        bool IReference<T>.TryGetValue(out T value) => TryGetValue(out value);
        T IReference<T>.Value => Value;
        
        object IReference.Value => Value;

        void IDisposable.Dispose() => Dispose();
    }
}
#endif