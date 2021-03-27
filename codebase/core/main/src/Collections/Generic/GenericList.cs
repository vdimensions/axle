#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
#if NETSTANDARD || NET35_OR_NEWER
using System.Linq;
#endif

namespace Axle.Collections.Generic
{
    /// <summary>
    /// A generic adapter for the non-generic <see cref="IList"/> collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the list.
    /// </typeparam>
    /// <seealso cref="IList{T}"/>
    /// <seealso cref="IList"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class GenericList<T> : IList<T>, IList, Axle.Collections.ReadOnly.IReadOnlyList<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList _underlyingCollection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<object, T> _converter;

        /// <summary>
        /// Creates a new instance of the <see cref="GenericList{T}"/> class.
        /// </summary>
        /// <param name="collection">The underlying collection to be exposed as generic.</param>
        /// <param name="converter">
        /// A conversion function that is used to turn the raw object elements of the underlying collection to the
        /// the generic type <typeparamref name="T"/>.
        /// </param>
        public GenericList(IList collection, Func<object, T> converter)
        {
            _underlyingCollection = collection ?? throw new ArgumentNullException(nameof(collection));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        /// <summary>
        /// Creates a new instance of the <see cref="GenericList{T}"/> class.
        /// </summary>
        /// <param name="collection">The underlying collection to be exposed as generic.</param>
        public GenericList(IList collection) : this(collection, x => (T) x) { }

        /// <inheritdoc />
        public void Add(T value) => _underlyingCollection.Add(value);

        /// <inheritdoc cref="IList{T}" />
        public void Clear() => _underlyingCollection.Clear();

        /// <inheritdoc />
        public bool Contains(T value) => _underlyingCollection.Contains(value);

        /// <inheritdoc />
        public void CopyTo(T[] array, int index)
        {
            for (var i = 0; i < _underlyingCollection.Count; i++)
            {
                array[i] = _converter(_underlyingCollection[i]);
            }
        }

        /// <inheritdoc />
        #if NETSTANDARD || NET35_OR_NEWER
        public IEnumerator<T> GetEnumerator() => _underlyingCollection.OfType<T>().GetEnumerator();
        #else
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T element in _underlyingCollection)
            {
                yield return element;
            }
        }
        #endif
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int IndexOf(T value) => _underlyingCollection.IndexOf(value);

        /// <inheritdoc />
        public void Insert(int index, T value) => _underlyingCollection.Insert(index, value);

        /// <inheritdoc />
        public bool Remove(T value)
        {
            var count = _underlyingCollection.Count;
            _underlyingCollection.Remove(value);
            return (count + 1) == _underlyingCollection.Count;
        }

        /// <inheritdoc cref="IList{T}"/>
        public void RemoveAt(int index) => _underlyingCollection.RemoveAt(index);

        /// <inheritdoc />
        public bool IsReadOnly => _underlyingCollection.IsReadOnly;

        /// <summary>
        /// Gets a value indicating whether the <see cref="GenericList{T}"/> has a fixed size.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the <see cref="GenericList{T}"/> has a fixed size; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFixedSize => _underlyingCollection.IsFixedSize;

        internal IList RawCollection => _underlyingCollection;

        /// <inheritdoc /> 
        public T this[int index]
        {
            get => _converter(_underlyingCollection[index]);
            set => _underlyingCollection[index] = value;
        }

        #region ICollection Implementation
        void ICollection.CopyTo(Array array, int index) => _underlyingCollection.CopyTo(array, index);

        /// <inheritdoc />
        int ICollection.Count => _underlyingCollection.Count;

        /// <inheritdoc />
        bool ICollection.IsSynchronized => _underlyingCollection.IsSynchronized;

        /// <inheritdoc />
        object ICollection.SyncRoot => _underlyingCollection.SyncRoot;

        /// <inheritdoc />
        int ICollection<T>.Count => _underlyingCollection.Count;
        #endregion
        
        #region IRadOnlyCollection Implementation
        #if NETSTANDARD
        /// <inheritdoc />
        int IReadOnlyCollection<T>.Count => _underlyingCollection.Count;
        #else
        int Axle.Collections.ReadOnly.IReadOnlyCollection<T>.Count => _underlyingCollection.Count;
        #endif
        #endregion

        #region IList Implementation
        /// <inheritdoc />
        int IList.Add(object value) => _underlyingCollection.Add(value);

        /// <inheritdoc />
        bool IList.Contains(object value) => _underlyingCollection.Contains(value);

        /// <inheritdoc />
        int IList.IndexOf(object value) => _underlyingCollection.IndexOf(value);

        /// <inheritdoc />
        void IList.Insert(int index, object value) => _underlyingCollection.Insert(index, value);

        /// <inheritdoc />
        void IList.Remove(object value) => _underlyingCollection.Remove(value);

        /// <inheritdoc />
        void IList.RemoveAt(int index) => _underlyingCollection.RemoveAt(index);

        /// <inheritdoc />
        bool IList.IsFixedSize => IsFixedSize;

        /// <inheritdoc />
        bool IList.IsReadOnly => IsReadOnly;

        /// <inheritdoc />
        object IList.this[int index]
        {
            get => _underlyingCollection[index];
            set => _underlyingCollection[index] = value;
        }
        #endregion
    }
}
#endif