using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections.Generic
{
    /// <summary>
    /// A generic adapter for the non-generic <see cref="IEnumerable"/>
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to enumerate. 
    /// </typeparam>
    /// <seealso cref="IEnumerable{T}"/>
    /// <seealso cref="IEnumerable"/>
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    public class GenericEnumerable<T> : IEnumerable<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable _innerCollection;

        public GenericEnumerable(IEnumerable collection)
        {
            _innerCollection = collection;
        }

        public IEnumerator<T> GetEnumerator() => new GenericEnumerator<T>(_innerCollection.GetEnumerator());
        IEnumerator IEnumerable.GetEnumerator() => _innerCollection.GetEnumerator();

        internal IEnumerable RawEnumerable => _innerCollection;
    }
}