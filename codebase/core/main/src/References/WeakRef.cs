#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.References
{
    /// Represents a weak reference; that is, a reference to an object that still allows the referenced
    /// object instance to be reclaimed by the garbage collector.
    /// This is the default implementation of the <see cref="IWeakReference{T}"/> interface.
    /// <seealso cref="IWeakReference{T}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public class WeakRef<T> : IWeakReference<T>, IEquatable<WeakRef<T>> where T: class
    {
        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> implementation that can compare weak references.
        /// Two weak references are deemed equal in case both are not alive or if their values are considered
        /// equal by the <see cref="P:WeakRef{T}.EqualityComparer.ValueComparer"/>. 
        /// </summary>
        /// <seealso cref="IEqualityComparer{T}"/>
        /// <seealso cref="WeakRef{T}"/>
        /// <seealso cref="IWeakReference{T}"/>
        public sealed class EqualityComparer : AbstractReferenceEqualityComparer<T, IWeakReference<T>>
        {
            /// <summary>
            /// Creates a new instance of the <see cref="EqualityComparer"/> class that uses
            /// a <see cref="ReferenceEqualityComparer{T}">default comparer</see> for comparing the reference value.
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

            private EqualityComparer() : base(new ReferenceEqualityComparer<T>()) { }
            private EqualityComparer(IEqualityComparer<T> valueComparer) : base(valueComparer) { }

            /// <inheritdoc />
            protected override bool DoEquals(IWeakReference<T> x, IWeakReference<T> y)
            {
                var xTarget = x.Value;
                var yTarget = y.Value;

                if (! x.IsAlive && ! y.IsAlive)
                {
                    return true;
                }

                if (! (xTarget is null || yTarget is null))
                {
                    return ValueComparer.Equals(xTarget, yTarget);
                }

                return false;
            }
        }
        
        #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
        private readonly WeakReference<T> _reference;

        private WeakRef(WeakReference<T> reference) { _reference = reference; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakRef{T}"/> class using the provided
        /// <paramref name="target"/> reference and uses the specified resurrection tracking. 
        /// </summary>
        /// <param name="target">
        /// The object to reference, or <c>null</c>. 
        /// </param>
        /// <param name="trackResurrection">
        /// <c>true</c> to track the object after finalization; <c>false</c> to track the object until finalization.
        /// </param>
        public WeakRef(T target, bool trackResurrection) : this(new WeakReference<T>(target, trackResurrection)) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakRef{T}"/> class using the provided
        /// <paramref name="target"/> reference. 
        /// </summary>
        /// <param name="target">
        /// The object to reference, or <c>null</c>. 
        /// </param>
        public WeakRef(T target) : this(new WeakReference<T>(target)) { }
        #else
        private readonly WeakReference _reference;

        private WeakRef(WeakReference reference) { _reference = reference; }
        /// <summary>
        /// Initializes a new <see cref="WeakRef{T}"/> instance that references the given <paramref name="target"/>
        /// object using the specified by the <paramref name="trackResurrection"/> resurrection tracking behavior. 
        /// </summary>
        /// <param name="target">
        /// The target object of this <see cref="WeakRef{T}"/>.
        /// </param>
        /// <param name="trackResurrection">
        /// Indicates when to stop tracking the <paramref name="target"/> object. <c>true</c> specifies that the object
        /// is tracked after finalization; <c>false</c> means the object is tracked until finalization.
        /// </param>
        public WeakRef(T target, bool trackResurrection) : this(new WeakReference(target, trackResurrection)) { }
        /// <summary>
        /// Initializes a new <see cref="WeakRef{T}"/> instance that references the given <paramref name="target"/>
        /// object using the default resurrection tracking. 
        /// </summary>
        /// <param name="target">
        /// The target object of this <see cref="WeakRef{T}"/>.
        /// </param>
        /// <remarks>
        /// Using this constructor is equivalent to calling the constructor overload
        /// <see cref="WeakRef{T}(T, Boolean)"/> with the <c>trackResurrection</c> parameter set to <c>false</c>.
        /// </remarks>
        public WeakRef(T target) : this(new WeakReference(target)) { }
        #endif
        
        /// <inheritdoc />
        public override bool Equals(object other)
        {
            switch (other)
            {
                case null when !TryGetValue(out _):
                    return true;
                case T otherVal:
                    return TryGetValue(out var val) && Equals(val, otherVal);
                case WeakRef<T> otherRef:
                    return EqualityComparer.Create().Equals(this, otherRef);
                case IWeakReference<T> otherReference:
                    return EqualityComparer.Create().Equals(this, otherReference);
                default:
                    return false;
            }
        }

        bool IEquatable<IWeakReference<T>>.Equals(IWeakReference<T> other) 
            => EqualityComparer.Create().Equals(this, other);

        bool IEquatable<WeakRef<T>>.Equals(WeakRef<T> other) 
            => EqualityComparer.Create().Equals(this, other);
        
        bool IEquatable<T>.Equals(T other) 
            => TryGetValue(out var v) && EqualityComparer.Create().ValueComparer.Equals(v, other);

        /// <inheritdoc />
        public override int GetHashCode() => EqualityComparer.Create().GetHashCode(this);

        /// <summary>
        /// Tries to retrieve the value that is referenced by the current <see cref="WeakRef{T}"/> instance.
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the value that has been assigned to the current
        /// <see cref="WeakRef{T}"/> instance, if it is available.
        /// This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the target value was retrieved; <c>false</c> otherwise.
        /// </returns>
        public bool TryGetValue(out T value)
        {
            #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
            return _reference.TryGetTarget(out value);
            #else
            value = Value;
            return IsAlive;
            #endif
        }

        /// <summary>
        /// Gets or sets the value of this <see cref="WeakRef{T}"/> instance.
        /// </summary>
        /// <returns>
        /// Returns the value of this <see cref="WeakRef{T}"/> instance.
        /// </returns>
        public T Value
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
                return _reference.TryGetTarget(out var xTarget) ? xTarget : null;
                #else
                return (T) _reference.Target;
                #endif
            }
            set
            {
                #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
                _reference.SetTarget(value);
                #else
                _reference.Target = value;
                #endif
            }
        }

        /// <inheritdoc cref="IWeakReference{T}.Value" />
        T IReference<T>.Value => Value;

        /// <inheritdoc cref="IWeakReference{T}.Value" />
        object IReference.Value => Value;
        
        /// <inheritdoc />
        bool IReference.HasValue => IsAlive;

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether the current <see cref="WeakRef{T}"/> instance
        /// points to a valid heap location.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                #if NETSTANDARD || NET45_OR_NEWER || UNITY_2018_1_OR_NEWER
                return _reference.TryGetTarget(out _);
                #else
                return _reference.IsAlive;
                #endif
            }
        }

        /// <inheritdoc cref="IWeakReference{T}.IsAlive" />
        bool IWeakReference<T>.IsAlive => IsAlive;
    }
}
#endif