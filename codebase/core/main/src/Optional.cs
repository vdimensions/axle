#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.References;
using Axle.Verification;


namespace Axle
{
    /// <summary>
    /// A static class that aids the functionality of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <seealso cref="Optional{T}"/>
    [Obsolete]
    public static class Optional
    {
        [Obsolete]
        public static Optional<T> Create<T>() { return Optional<T>.Undefined; }

        [Obsolete]
        public static Optional<T> From<T>(T value)
        {
            return value == null ? Optional<T>.Undefined : new Optional<T>(value);
        }

        [Obsolete]
        public static Optional<T> From<T>(T? value) where T : struct
        {
            return value.HasValue ? new Optional<T>(value.Value) : Optional<T>.Undefined;
        }

        [Obsolete]
        public static Optional<T> Unwrap<T>(Optional<T?> value) where T : struct
        {
            return value.HasValue ? From(value.Value) : Optional<T>.Undefined;
        }

        [Obsolete]
        public static IEnumerable<T> FilterPresent<T>(this IEnumerable<Optional<T>> collection)
        {
            return Enumerable.Select(
                Enumerable.Where(
                    Verifier.IsNotNull(Verifier.VerifyArgument(collection, nameof(collection))).Value, 
                    x => x.HasValue), 
                x => x.Value);
        }

        [Obsolete]
        public static Optional<TResult> Next<T, TResult>(this Optional<T> arg, Func<T, TResult> map)
        {
            return arg ? Optional.From(map(arg.Value)) : Optional.Create<TResult>();
        }
        [Obsolete]
        public static Optional<TResult> Next<T, TResult>(this Optional<T> arg, Func<T, Optional<TResult>> map)
        {
            return arg ? map(arg.Value) : Optional.Create<TResult>();
        }
    }

    /// <summary>
    /// A container object for a non-null value. The <see cref="Optional{T}.HasValue"/> property indicates whether there
    /// is a value available.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    [Obsolete("Use Nullable<_> for value types and Nullsafe<_> for reference types instead.")]
    public struct Optional<T> : IEquatable<Optional<T>>, IEquatable<T>, IReference<T>
    {
        public static readonly Optional<T> Undefined = new Optional<T>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool _isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{T}"/> <see langword="struct"/>.
        /// </summary>
        /// <param name="value">Value.</param>
        internal Optional(T value)
        {
            _isSet = (_value = value) != null;
        }

        public override bool Equals(object other) => other is Optional<T> optional && optional.Equals (this);
        public bool Equals(Optional<T> other) => _isSet ? other._isSet && Equals(_value, other._value) : !other._isSet;
        bool IEquatable<T>.Equals(T other) => _isSet && Equals(_value, other);

        public override int GetHashCode() => (_value == null ? 0 : _value.GetHashCode()) ^ typeof(T).GetHashCode();

        public T GetValueOrDefault() => GetValueOrDefault (default(T));
        public T GetValueOrDefault(T defaultValue) => _isSet ? _value : defaultValue;

//        public TResult TryInvoke<TResult>(Func<T, TResult> func, TResult defaultValue)
//        {
//            return isSet ? func (value) : defaultValue;
//        }
//        public TResult TryInvoke<TResult>(Func<T, TResult> func) { return TryInvoke(func, default(TResult)); }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Axle.Optional{T}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current <see cref="Axle.Optional{T}"/>.
        /// </returns>
        public override string ToString () => _isSet ? (_value == null ? "null" : _value.ToString()) : "[Undefined]";

        bool IReference<T>.TryGetValue(out T value)
        {
            value = _value;
            return _isSet;
        }

        //[CanBeNull(false)]
        /// <summary>
        /// Gets the value of the underlying object represented by the current <see cref="Optional{T}"/> instance, or
        /// <c>null</c> if there is no value present.
        /// </summary>
        /// <value>
        /// A reference to the underlying object represented by the current <see cref="Optional{T}"/> instance, or
        /// <c>null</c> if there is no value present.
        /// </value>
        public T Value => _value;
        object IReference.Value => Value;

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether this <see cref="Optional{T}"/> instance has any value.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue => _isSet;

        public static bool operator false(Optional<T> optional) => !optional.HasValue;
        public static bool operator true(Optional<T> optional) => optional.HasValue;
        public static T operator ~(Optional<T> optional) => optional.Value;

        public static implicit operator Optional<T> (T value) => value == null ? Undefined : new Optional<T>(value);
        public static explicit operator T (Optional<T> optional) => optional.GetValueOrDefault();

        public static string operator % (string format, Optional<T> optional)
        {
            return string.Format(format, optional.HasValue ? (object) optional.Value : optional.ToString());
        }
        /* EXPERIMENTAL
        public static Optional<dynamic> operator / (Optional<T> optional, Func<T, dynamic> func)
        {
            return optional.Next(func);
        }
        */
    }
}
#endif