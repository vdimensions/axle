#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


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
    #if !NETSTANDARD
    [Serializable]
    #endif
    public sealed class GenericList<T> : IList<T>, IList
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList _underlyingCollection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<object, T> _converter;

        public GenericList(IList collection, Func<object, T> converter)
        {
            _underlyingCollection = collection ?? throw new ArgumentNullException(nameof(collection));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        public GenericList(IList collection) : this(collection, x => (T) x) { }

        /// <inheritdoc />
        public void Add(T value) { _underlyingCollection.Add(value); }

        /// <inheritdoc cref="IList{T}" />
        public void Clear() { _underlyingCollection.Clear(); }

        /// <inheritdoc />
        public bool Contains(T value) { return _underlyingCollection.Contains(value); }

        /// <inheritdoc />
        public void CopyTo(T[] array, int index)
        {
            for (var i = 0; i < _underlyingCollection.Count; i++)
            {
                array[i] = _converter(_underlyingCollection[i]);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() { return _underlyingCollection.OfType<T>().GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <inheritdoc />
        public int IndexOf(T value) { return _underlyingCollection.IndexOf(value); }

        /// <inheritdoc />
        public void Insert(int index, T value) { _underlyingCollection.Insert(index, value); }

        /// <inheritdoc />
        public bool Remove(T value)
        {
            var count = _underlyingCollection.Count;
            _underlyingCollection.Remove(value);
            return (count + 1) == _underlyingCollection.Count;
        }

        /// <inheritdoc cref="IList{T}"/>
        public void RemoveAt(int index) { _underlyingCollection.RemoveAt(index); }

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

        int ICollection.Count => _underlyingCollection.Count;
        bool ICollection.IsSynchronized => _underlyingCollection.IsSynchronized;
        object ICollection.SyncRoot => _underlyingCollection.SyncRoot;
        int ICollection<T>.Count => _underlyingCollection.Count;
        #endregion

        #region IList Implementation
        int IList.Add(object value) => _underlyingCollection.Add(value);

        bool IList.Contains(object value) => _underlyingCollection.Contains(value);

        int IList.IndexOf(object value) => _underlyingCollection.IndexOf(value);

        void IList.Insert(int index, object value) => _underlyingCollection.Insert(index, value);

        void IList.Remove(object value) => _underlyingCollection.Remove(value);

        void IList.RemoveAt(int index) => _underlyingCollection.RemoveAt(index);

        bool IList.IsFixedSize => IsFixedSize;
        bool IList.IsReadOnly => IsReadOnly;

        object IList.this[int index]
        {
            get => _underlyingCollection[index];
            set => _underlyingCollection[index] = value;
        }
        #endregion
    }
}
#endif