#if (NETSTANDARD1_4_OR_NEWER || NET46_OR_NEWER) && !NET45
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Axle.Reflection;


namespace Axle.References
{
    /// <summary>
    /// Represents a reference to ambient data, that is local to a given asynchronous control flow, such as asynchronous method. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class AsyncLocalReference<T> : IAsyncLocalReference<T>
    {
        private struct AsyncLocalRefValue
        {
            private AsyncLocalRefValue(T value, bool hasValue)
            {
                Value = value;
                HasValue = hasValue;
            }
            public AsyncLocalRefValue(T value) : this(value, true) { }

            public T Value { get; }
            public bool HasValue { get; }
        }

        private static class AsyncLocalValueChangedArgsConstructor
        {
            [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
            private static readonly IConstructor _constructor;

            static AsyncLocalValueChangedArgsConstructor()
            {
                var typeofT = typeof(T);
                _constructor = new DefaultIntrospector<AsyncLocalValueChangedArgs<T>>().GetConstructor(ScanOptions.NonPublic, typeofT, typeofT, typeof(bool));
            }

            public static AsyncLocalValueChangedArgs<T> Invoke(T previousValue, T currentValue, bool contextChanged)
            {
                return (AsyncLocalValueChangedArgs<T>) _constructor.Invoke(previousValue, currentValue, contextChanged);
            }
        }

        private static void MapValueChangedHandler(AsyncLocalValueChangedArgs<AsyncLocalRefValue> x, Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
        {
            var args = AsyncLocalValueChangedArgsConstructor.Invoke(x.PreviousValue.Value, x.CurrentValue.Value, x.ThreadContextChanged);
            valueChangedHandler(args);
        }

        private readonly AsyncLocal<AsyncLocalRefValue> _t;

        private AsyncLocalReference(AsyncLocal<AsyncLocalRefValue> t) => _t = t;
        /// <summary>
        /// Creates a <see cref="AsyncLocalReference{T}"/> instance that does not receive change notifications.
        /// </summary>
        public AsyncLocalReference() : this(new AsyncLocal<AsyncLocalRefValue>()) { }
        /// <summary>
        /// Creates a <see cref="AsyncLocalReference{T}"/> instance that receives change notifications.
        /// </summary>
        /// <param name="valueChangedHandler">
        /// The delegate that is called whenever the current value changes on any thread.
        /// </param>
        public AsyncLocalReference(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
            : this(valueChangedHandler == null ? throw new ArgumentNullException(nameof(valueChangedHandler)) : new AsyncLocal<AsyncLocalRefValue>(x => MapValueChangedHandler(x, valueChangedHandler))) { }

        /// <summary>
        /// Gets or sets the value of the ambient data.
        /// </summary>
        public T Value
        {
            get => _t.Value.Value;
            set => _t.Value = new AsyncLocalRefValue(value);
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="Value"/> has been initialized for the current asynchronous context.
        /// </summary>
        public bool HasValue => _t.Value.HasValue;

        bool IReference<T>.TryGetValue(out T value)
        {
            if (_t.Value.HasValue)
            {
                value = _t.Value.Value;
                return true;
            }
            value = default(T);
            return false;
        }
        T IReference<T>.Value => Value;
        object IReference.Value => Value;
    }
}
#endif