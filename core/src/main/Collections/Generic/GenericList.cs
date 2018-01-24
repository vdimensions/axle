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
    #if !netstandard
    [Serializable]
    #endif
    public sealed class GenericList<T> : IList<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList underlyingCollection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<object, T> converter;

        public GenericList(IList collection, Func<object, T> converter)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            this.underlyingCollection = collection;
            this.converter = converter;
        }
        public GenericList(IList collection) : this(collection, x => (T) x) { }

        /// <inheritdoc />
        public void Add(T value) { underlyingCollection.Add(value); }

        /// <inheritdoc />
        public void Clear() { underlyingCollection.Clear(); }

        /// <inheritdoc />
        public bool Contains(T value) { return underlyingCollection.Contains(value); }

        /// <inheritdoc />
        public void CopyTo(T[] array, int index)
        {
            for (var i = 0; i < underlyingCollection.Count; i++)
            {
                array[i] = converter(underlyingCollection[i]);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() { return underlyingCollection.OfType<T>().GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <inheritdoc />
        public int IndexOf(T value) { return underlyingCollection.IndexOf(value); }

        /// <inheritdoc />
        public void Insert(int index, T value) { underlyingCollection.Insert(index, value); }

        /// <inheritdoc />
        public bool Remove(T value)
        {
            var count = underlyingCollection.Count;
            underlyingCollection.Remove(value);
            return (count + 1) == underlyingCollection.Count;
        }

        /// <inheritdoc />
        public void RemoveAt(int index) { underlyingCollection.RemoveAt(index); }

        int ICollection<T>.Count { get { return underlyingCollection.Count; } }
        /// <inheritdoc />
        public bool IsReadOnly { get { return underlyingCollection.IsReadOnly; } }
        public bool IsFixedSize { get { return underlyingCollection.IsFixedSize; } }
        internal IList RawCollection { get { return underlyingCollection; } }

        /// <inheritdoc />
        public T this[int index]
        {
            get { return converter(underlyingCollection[index]); }
            set { underlyingCollection[index] = value; }
        }
    }
}