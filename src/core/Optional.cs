using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.References;
using Axle.Verification;
//using Axle.CodeDescriptors;


namespace Axle
{
    /// <summary>
    /// A static class that aids the functionality of <see cref="Optional`1"/> instances.
    /// </summary>
    /// <seealso cref="Optional`1"/>
	public static class Optional
	{
		public static Optional<T> Create<T>() { return Optional<T>.Undefined; }

		public static Optional<T> From<T>(T value) { return value == null ? Optional<T>.Undefined : new Optional<T>(value); }
		public static Optional<T> From<T>(T? value) where T: struct { return value.HasValue ? new Optional<T>(value.Value) : Optional<T>.Undefined; }

		public static Optional<T> Unwrap<T>(Optional<T?> value) where T: struct { return value.HasValue ? From(value.Value) : Optional<T>.Undefined; }

		public static IEnumerable<T> FilterPresent<T>(this IEnumerable<Optional<T>> collection)
		{
            return collection.VerifyArgument(nameof(collection)).IsNotNull().Value.Where(x => x.HasValue).Select(x => x.Value);
		}

        public static Optional<TResult> Next<T, TResult>(this Optional<T> arg, Func<T, TResult> map)
        {
            return arg ? Optional.From(map(arg.Value)) : Optional.Create<TResult>();
        }
        public static Optional<TResult> Next<T, TResult>(this Optional<T> arg, Func<T, Optional<TResult>> map)
        {
            return arg ? map(arg.Value) : Optional.Create<TResult>();
        }
	}

    /// <summary>
    /// A container object for a non-null value. The <see cref="Optional{T}.HasValue"/> property indicates whether there is a value available.
    /// </summary>
#if !netstandard
    [Serializable]
#endif
	public struct Optional<T> : IEquatable<Optional<T>>, IEquatable<T>, IReference<T>
	{
		public static readonly Optional<T> Undefined = new Optional<T>();

	    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly T value;
	    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional`1"/> struct.
        /// </summary>
        /// <param name="value">Value.</param>
		internal Optional(T value)
		{
            this.isSet = (this.value = value) != null;
		}

		public override bool Equals(object other) { return other is Optional<T> && ((Optional<T>) other).Equals (this); }
		public bool Equals(Optional<T> other) { return this.isSet ? other.isSet && Equals(this.value, other.value) : !other.isSet; }
		bool IEquatable<T>.Equals(T other) { return this.isSet && Equals(this.value, other); }

		public override int GetHashCode() { return (this.value == null ? 0 : this.value.GetHashCode()) ^ typeof(T).GetHashCode(); }

		public T GetValueOrDefault() { return GetValueOrDefault (default(T)); }
		public T GetValueOrDefault(T defaultValue) { return this.isSet ? this.value : defaultValue; }

//		public TResult TryInvoke<TResult>(Func<T, TResult> func, TResult defaultValue)
//		{
//			return isSet ? func (value) : defaultValue;
//		}
//		public TResult TryInvoke<TResult>(Func<T, TResult> func) { return TryInvoke(func, default(TResult)); }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Axle.Optional`1"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents the current <see cref="Axle.Optional`1"/>.
        /// </returns>
		public override string ToString () { return this.isSet ? (this.value == null ? "null" : this.value.ToString()) : "[Undefined]"; }

        //[CanBeNull(false)]
        /// <summary>
        /// Gets the value of the underlying object represented by the current <see cref="Optional{T}"/> instance, or <c>null</c> if there is no value present.
        /// </summary>
        /// <value>
        /// A reference to the underlying object represented by the current <see cref="Optional{T}"/> instance, or <c>null</c> if there is no value present.
        /// </value>
		public T Value { get { return this.value; } }
        object IReference.Value { get { return this.Value; } }

        /// <summary>
        /// Gets a <see cref="bool"/> value indicating whether this <see cref="Optional{T}"/> instance has any value.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue { get { return this.isSet; } }

        public static bool operator false(Optional<T> optional) { return !optional.HasValue; }
        public static bool operator true(Optional<T> optional) { return optional.HasValue; }
        public static T operator ~(Optional<T> optional) { return optional.Value; }

		public static implicit operator Optional<T> (T value) { return value == null ? Undefined : new Optional<T>(value); }
		public static explicit operator T (Optional<T> optional) { return optional.GetValueOrDefault(); }
	}
}

