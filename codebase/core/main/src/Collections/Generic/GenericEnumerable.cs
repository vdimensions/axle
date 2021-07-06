#if NETSTANDARD || NET20_OR_NEWER
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
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public class GenericEnumerable<T> : IEnumerable<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable _innerCollection;

        /// <summary>
        /// Creates a new instance of the <see cref="GenericEnumerable{T}"/> class.
        /// </summary>
        /// <param name="collection">
        /// The underlying collection to be exposed as generic.
        /// </param>
        public GenericEnumerable(IEnumerable collection)
        {
            _innerCollection = collection;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new GenericEnumerator<T>(_innerCollection.GetEnumerator());
        IEnumerator IEnumerable.GetEnumerator() => _innerCollection.GetEnumerator();

        internal IEnumerable RawEnumerable => _innerCollection;
    }
}
#endif