#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.References
{
    /// <summary>
    /// A container object for a non-null value. This object has the same semantics as the <see cref="Nullable{T}"/>
    /// struct, but is relevant only on reference types.
    /// The <see cref="Nullsafe{T}.HasValue"/> property indicates whether there is a value available.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public struct Nullsafe<T> : IEquatable<Nullsafe<T>>, IEquatable<T>, IReference<T>
        where T: class
    {
        /// <summary>
        /// An empty <see cref="Nullsafe{T}"/> value representing a <c>null</c> reference.
        /// </summary>
        public static readonly Nullsafe<T> None = new Nullsafe<T>();

        /// <summary>
        /// Creates a new <see cref="Nullsafe{T}"/> instance with the provided <paramref name="value"/>. 
        /// </summary>
        /// <param name="value">
        /// The value for the <see cref="Nullsafe{T}"/> instance to be created
        /// </param>
        /// <returns>
        /// A new <see cref="Nullsafe{T}"/> instance with the provided <paramref name="value"/>. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static Nullsafe<T> Some(T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value)));
            return new Nullsafe<T>(value);
        }

        public static explicit operator T(Nullsafe<T> ns) => ns.Value;
        public static implicit operator Nullsafe<T>(T value) => new Nullsafe<T>(value);
        //public static implicit operator Nullsafe<T>(Nullable<Nullsafe<T>> nv) => nv.GetValueOrDefault();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool _isSet;

        private Nullsafe(T value)
        {
            _isSet = !ReferenceEquals(value, null);
            _value = value;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            switch (other)
            {
                case null when !_isSet:
                    return true;
                case T val:
                    return Equals(val, _value);
                case Nullsafe<T> ns:
                    return ns.Equals(this);
                default:
                    return false;
            }
        }
        public bool Equals(Nullsafe<T> other) => _isSet ? other._isSet && Equals(_value, other._value) : !other._isSet;
        bool IEquatable<T>.Equals(T other) => Equals(_value, other);

        public override int GetHashCode() => _value == null ? 0 : _value.GetHashCode();

        public bool TryGetValue(out T value)
        {
            value = _value;
            return value != null;
        }

        /// <summary>
        /// Gets the value of the underlying object represented by the current <see cref="Nullsafe{T}"/> instance.
        /// </summary>
        /// <value>
        /// A reference to the underlying object represented by the current <see cref="Nullsafe{T}"/> instance.
        /// </value>
        public T Value => _value;
        object IReference.Value => _value;

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether this <see cref="Nullsafe{T}"/> instance has any value.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue => _isSet;
    }
}
#endif