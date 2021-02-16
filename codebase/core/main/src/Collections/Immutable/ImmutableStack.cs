using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD
using System.Linq;
#endif
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    public static class ImmutableStack
    {
        public static ImmutableStack<T> Create<T>()
        {
            #if NETSTANDARD
            return new ImmutableStack<T>(System.Collections.Immutable.ImmutableStack.Create<T>());
            #else
            return new ImmutableStack<T>(new Stack<T>());
            #endif
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static ImmutableStack<T> CreateRange<T>(IEnumerable<T> items)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            
            #if NETSTANDARD
            return new ImmutableStack<T>(System.Collections.Immutable.ImmutableStack.CreateRange<T>(items));
            #else
            return new ImmutableStack<T>(new Stack<T>(items));
            #endif
        }
    }

    public class ImmutableStack<T> : IImmutableStack<T>
    {
        public static readonly ImmutableStack<T> Empty = ImmutableStack.Create<T>();
        
        #if NETSTANDARD
        private readonly System.Collections.Immutable.ImmutableStack<T> _impl;

        internal ImmutableStack(System.Collections.Immutable.ImmutableStack<T> impl)
        {
            _impl = impl;
        }
        #else
        private readonly Stack<T> _impl;

        internal ImmutableStack(Stack<T> impl)
        {
            _impl = impl;
        }
        #endif

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>) _impl).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public ImmutableStack<T> Clear()
        {
            #if NETSTANDARD
            return ImmutableStack.CreateRange(_impl.Clear());
            #else
            return ImmutableStack.Create<T>();
            #endif
        }
        IImmutableStack<T> IImmutableStack<T>.Clear() => Clear();

        /// <inheritdoc />
        public ImmutableStack<T> Push(T value)
        {
            #if NETSTANDARD
            return ImmutableStack.CreateRange(_impl.Push(value));
            #else
            var result = new Stack<T>(_impl);
            result.Push(value);
            return new ImmutableStack<T>(result);
            #endif
        }
        IImmutableStack<T> IImmutableStack<T>.Push(T value) => Push(value);

        /// <inheritdoc />
        public ImmutableStack<T> Pop()
        {
            #if NETSTANDARD
            return ImmutableStack.CreateRange(_impl.Pop());
            #else
            var result = new Stack<T>(_impl);
            result.Pop();
            return new ImmutableStack<T>(result);
            #endif
        }
        IImmutableStack<T> IImmutableStack<T>.Pop() => Pop();

        /// <inheritdoc />
        public ImmutableStack<T> Pop(out T value)
        {
            #if NETSTANDARD
            return ImmutableStack.CreateRange(_impl.Pop(out value));
            #else
            var result = new Stack<T>(_impl);
            value = result.Pop();
            return new ImmutableStack<T>(result);
            #endif
        }
        IImmutableStack<T> IImmutableStack<T>.Pop(out T value) => Pop(out value);

        /// <inheritdoc />
        public T Peek() => _impl.Peek();

        /// <inheritdoc />
        public bool IsEmpty
        {
            #if NETSTANDARD
            get { return _impl.IsEmpty; }
            #else
            get { return _impl.Count == 0; }
            #endif
        }

        #if NETSTANDARD
        /// <inheritdoc />
        public int Count => this.Count();
        #else
        public int Count => _impl.Count;
        #endif
    }
}