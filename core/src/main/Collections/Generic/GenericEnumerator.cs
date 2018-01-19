using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
    #if !netstandard
    [Serializable]
    #endif
    internal class GenericEnumerator : GenericEnumerator<object>
    {
        public GenericEnumerator(IEnumerator enumerator) : base(enumerator) { }
    }

    #if !netstandard
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

        public void Dispose()
        {
            var enumratorAsIDisposalbe = nonGenericEnumerator as IDisposable;
            if (enumratorAsIDisposalbe != null)
            {
                enumratorAsIDisposalbe.Dispose();
            }
        }
        public bool MoveNext() { return nonGenericEnumerator.MoveNext(); }
        public void Reset() { nonGenericEnumerator.Reset(); }

        public T Current { get { return (T) nonGenericEnumerator.Current; } }
        object IEnumerator.Current { get { return nonGenericEnumerator.Current; } }
    }

    #if !netstandard
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

        public void Dispose()
        {
            var enumratorAsIDisposalbe = innerEnumerator as IDisposable;
            if (enumratorAsIDisposalbe != null)
            {
                enumratorAsIDisposalbe.Dispose();
            }
            converter = null;
        }
        public bool MoveNext() { return innerEnumerator.MoveNext(); }
        public void Reset() { innerEnumerator.Reset(); }

        public T2 Current { get { return converter(innerEnumerator.Current); } }
        object IEnumerator.Current { get { return innerEnumerator.Current; } }
    }
}
