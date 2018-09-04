#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal class GenericEnumerator : GenericEnumerator<object>
    {
        public GenericEnumerator(IEnumerator enumerator) : base(enumerator) { }
    }

    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class GenericEnumerator<T> : IEnumerator<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator _nonGenericEnumerator;

        public GenericEnumerator(IEnumerator enumerator)
        {
            _nonGenericEnumerator = enumerator;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_nonGenericEnumerator is IDisposable enumratorAsIDisposalbe)
            {
                enumratorAsIDisposalbe.Dispose();
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

    #if NETSTANDARD || NET35_OR_NEWER
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class GenericEnumerator<T1, T2> : IEnumerator<T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator<T1> _innerEnumerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Func<T1, T2> _converter;

        public GenericEnumerator(IEnumerator<T1> enumerator, Func<T1, T2> converter)
        {
            _innerEnumerator = enumerator;
            _converter = converter;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_innerEnumerator is IDisposable enumratorAsIDisposalbe)
            {
                enumratorAsIDisposalbe.Dispose();
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
    #endif
}
#endif
