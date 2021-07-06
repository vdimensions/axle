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

        /// <summary>
        /// Enables implicit conversion between a <see cref="Nullsafe{T}"/> instance and the underlying type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="ns">
        /// The <see cref="Nullsafe{T}"/> operand.
        /// </param>
        /// <returns>
        /// An instance of the <typeparamref name="T"/> obtained from the value of the <see cref="Nullsafe{T}"/>
        /// operand.
        /// </returns>
        public static explicit operator T(Nullsafe<T> ns) => ns.Value;
        /// <summary>
        /// Enables implicit conversion between an instance of <typeparamref name="T"/> and <see cref="Nullsafe{T}"/>.
        /// </summary>
        /// <param name="value">
        /// The value operand.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Nullsafe{T}"/> obtained from the provided <paramref name="value"/>
        /// operand.
        /// </returns>
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

        /// <inheritdoc />
        public bool Equals(Nullsafe<T> other) => 
            _isSet 
                ? other._isSet && Equals(_value, other._value) 
                : !other._isSet;
        bool IEquatable<T>.Equals(T other) => Equals(_value, other);

        /// <inheritdoc />
        public override int GetHashCode() => _value == null ? 0 : _value.GetHashCode();

        /// <summary>
        /// Attempts to retrieve the value that is referenced by the current <see cref="Nullsafe{T}"/> object.
        /// </summary>
        /// <param name="value">
        /// When this method returns, contains the value that has been created for the current <see cref="Nullsafe{T}"/>
        /// instance, if it was not a <c>null</c> reference.
        /// This parameter is treated as uninitialized.
        /// </param>
        /// <returns>
        /// <c>true</c> if the value was retrieved;
        /// <c>false</c> otherwise.
        /// </returns>
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
        /// Gets a <see cref="bool"/> value indicating whether this <see cref="Nullsafe{T}"/> instance's value is not a
        /// <c>null</c> reference.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance's <see cref="Value"/> is <em>not</em> a <c>null</c> reference;
        /// <c>false</c> otherwise.
        /// </value>
        public bool HasValue => _isSet;
    }
}
#endif