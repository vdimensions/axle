#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
    /// <summary>
    /// A generic decorator for the non-generic <see cref="IEnumerator"/> interface.
    /// This type is equivalent to the <see cref="GenericEnumerator{T}"/> where <c>T</c> is <see cref="System.Object"/>.
    /// </summary>
    /// <seealso cref="IEnumerator{T}"/>
    /// <seealso cref="IEnumerator"/>
    /// <seealso cref="GenericEnumerator{T}"/>
    /// <seealso cref="GenericEnumerator{T1, T2}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal class GenericEnumerator : GenericEnumerator<object>
    {
        /// <inheritdoc />
        public GenericEnumerator(IEnumerator enumerator) : base(enumerator) { }
    }

    /// <summary>
    /// A generic decorator for the non-generic <see cref="IEnumerator"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements to enumerate.
    /// </typeparam>
    /// <seealso cref="IEnumerator{T}"/>
    /// <seealso cref="IEnumerator"/>
    /// <seealso cref="GenericEnumerator{T1, T2}"/>
    /// <seealso cref="GenericEnumerator"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class GenericEnumerator<T> : IEnumerator<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator _nonGenericEnumerator;

        /// <summary>
        /// Creates a new instance of the <see cref="GenericEnumerator{T}"/> class.
        /// </summary>
        /// <param name="enumerator">
        /// The underlying <see cref="IEnumerator"/> to be represented as a generic <see cref="IEnumerator{T}"/>.
        /// </param>
        public GenericEnumerator(IEnumerator enumerator)
        {
            _nonGenericEnumerator = enumerator;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_nonGenericEnumerator is IDisposable enumeratorAsIDisposable)
            {
                enumeratorAsIDisposable.Dispose();
            }
        }
        /// <inheritdoc />
        public bool MoveNext() { return _nonGenericEnumerator.MoveNext(); }
        /// <inheritdoc />
        public void Reset() { _nonGenericEnumerator.Reset(); }

        /// <inheritdoc />
        public T Current => (T) _nonGenericEnumerator.Current;
        /// <inheritdoc />
        object IEnumerator.Current => _nonGenericEnumerator.Current;
    }

    /// <summary>
    /// A decorator around an <see cref="IEnumerator{T1}"/> instance that
    /// can be used to expose it as a <see cref="IEnumerator{T2}"/>.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of elements in the underlying enumerator.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of elements for the exposed enumerator.
    /// </typeparam>
    /// <seealso cref="IEnumerator{T}"/>
    /// <seealso cref="IEnumerator"/>
    /// <seealso cref="GenericEnumerator{T}"/>
    /// <seealso cref="GenericEnumerator"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class GenericEnumerator<T1, T2> : IEnumerator<T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator<T1> _innerEnumerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Func<T1, T2> _converter;

        /// <summary>
        /// Creates a new instance of the <see cref="GenericEnumerator{T1, T2}"/> class.
        /// </summary>
        /// <param name="enumerator">
        /// The <see cref="IEnumerator{T1}"/> instance to be represented as a <see cref="IEnumerator{T2}"/>
        /// </param>
        /// <param name="converter">
        /// A <see cref="Func{T1, T2}"/> that is used to convert elements of type <typeparamref name="T1"/> to type <typeparamref name="T2"/>
        /// </param>
        public GenericEnumerator(IEnumerator<T1> enumerator, Func<T1, T2> converter)
        {
            _innerEnumerator = enumerator;
            _converter = converter;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_innerEnumerator is IDisposable enumeratorAsIDisposable)
            {
                enumeratorAsIDisposable.Dispose();
            }
            _converter = null;
        }
        /// <inheritdoc />
        public bool MoveNext() { return _innerEnumerator.MoveNext(); }
        /// <inheritdoc />
        public void Reset() { _innerEnumerator.Reset(); }

        /// <inheritdoc />
        public T2 Current => _converter(_innerEnumerator.Current);
        /// <inheritdoc />
        object IEnumerator.Current => _innerEnumerator.Current;
    }
}
#endif
