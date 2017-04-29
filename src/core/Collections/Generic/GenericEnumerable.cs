using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
#if !netstandard
    [Serializable]
#endif
    public class GenericEnumerable<T> : IEnumerable<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable innerCollection;

        public GenericEnumerable(IEnumerable collection)
        {
            innerCollection = collection;
        }

        public IEnumerator<T> GetEnumerator() { return new GenericEnumerator<T>(innerCollection.GetEnumerator()); }
        IEnumerator IEnumerable.GetEnumerator() { return innerCollection.GetEnumerator(); }

        internal IEnumerable RawEnumerable { get { return innerCollection; } }
    }
}