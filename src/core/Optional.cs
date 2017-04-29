using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.References;
using Axle.Verification;
//using Axle.CodeDescriptors;


namespace Axle
{
	public static class Optional
	{
		public static Optional<T> Create<T>() { return Optional<T>.Undefined; }

		public static Optional<T> From<T>(T value) { return value == null ? Optional<T>.Undefined : new Optional<T>(value); }
		public static Optional<T> From<T>(T? value) where T: struct { return value.HasValue ? new Optional<T>(value.Value) : Optional<T>.Undefined; }

		public static Optional<T> Unwrap<T>(Optional<T?> value) where T: struct { return value.HasValue ? From(value.Value) : Optional<T>.Undefined; }

		public static IEnumerable<T> FilterPresent<T>(this IEnumerable<Optional<T>> collection)
		{
			return collection.VerifyArgument("collection").IsNotNull().Value.Where(x => x.HasValue).Select(x => x.Value);
		}
	}

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

		internal Optional(T value)
		{
			this.value = value;
			this.isSet = true;
		}

		public override bool Equals(object other) { return other is Optional<T> && ((Optional<T>) other).Equals (this); }
		public bool Equals(Optional<T> other) { return this.isSet ? other.isSet && Equals (this.value, other.value) : !other.isSet; }
		bool IEquatable<T>.Equals(T other) { return this.isSet && Equals(this.value, other); }

		public override int GetHashCode() { return (this.value == null ? 0 : this.value.GetHashCode()) ^ typeof(T).GetHashCode(); }

		public T GetValueOrDefault() { return GetValueOrDefault (default(T)); }
		public T GetValueOrDefault(T defaultValue) { return this.isSet ? this.value : defaultValue; }

//		public TResult TryInvoke<TResult>(Func<T, TResult> func, TResult defaultValue)
//		{
//			return isSet ? func (value) : defaultValue;
//		}
//		public TResult TryInvoke<TResult>(Func<T, TResult> func) { return TryInvoke(func, default(TResult)); }

		public override string ToString () { return this.isSet ? (this.value == null ? "null" : this.value.ToString()) : "[Undefined]"; }

        //[CanBeNull(false)]
		public T Value { get { return this.value; } }
        object IReference.Value { get { return this.Value; } }

        public bool HasValue { get { return this.isSet; } }

        public static bool operator false(Optional<T> optional) { return !optional.HasValue; }
        public static bool operator true(Optional<T> optional) { return optional.HasValue; }
        public static T operator ~(Optional<T> optional) { return optional.Value; }

		public static implicit operator Optional<T> (T value) { return value == null ? Undefined : new Optional<T>(value); }
		public static explicit operator T (Optional<T> optional) { return optional.GetValueOrDefault(); }
	}
}

