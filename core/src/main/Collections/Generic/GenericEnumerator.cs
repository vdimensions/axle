using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    internal class GenericEnumerator : GenericEnumerator<object>
    {
        public GenericEnumerator(IEnumerator enumerator) : base(enumerator) { }
    }

    #if !NETSTANDARD
    [Serializable]
    #endif
    public class GenericEnumerator<T> : IEnumerator<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator nonGenericEnumerator;

        public GenericEnumerator(IEnumerator enumerator)
        {
            nonGenericEnumerator = enumerator;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (nonGenericEnumerator is IDisposable enumratorAsIDisposalbe)
            {
                enumratorAsIDisposalbe.Dispose();
            }
        }
        /// <inheritdoc />
        public bool MoveNext() { return nonGenericEnumerator.MoveNext(); }
        /// <inheritdoc />
        public void Reset() { nonGenericEnumerator.Reset(); }

        /// <inheritdoc />
        public T Current => (T) nonGenericEnumerator.Current;
        /// <inheritdoc />
        object IEnumerator.Current => nonGenericEnumerator.Current;
    }

    #if NETSTANDARD || NET35_OR_NEWER
    #if !NETSTANDARD
    [Serializable]
    #endif
    public class GenericEnumerator<T1, T2> : IEnumerator<T2>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerator<T1> innerEnumerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Func<T1, T2> converter;

        public GenericEnumerator(IEnumerator<T1> enumerator, Func<T1, T2> converter)
        {
            innerEnumerator = enumerator;
            this.converter = converter;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (innerEnumerator is IDisposable enumratorAsIDisposalbe)
            {
                enumratorAsIDisposalbe.Dispose();
            }
            converter = null;
        }
        /// <inheritdoc />
        public bool MoveNext() { return innerEnumerator.MoveNext(); }
        /// <inheritdoc />
        public void Reset() { innerEnumerator.Reset(); }

        /// <inheritdoc />
        public T2 Current => converter(innerEnumerator.Current);
        /// <inheritdoc />
        object IEnumerator.Current => innerEnumerator.Current;
    }
    #endif
}
