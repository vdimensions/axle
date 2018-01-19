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

        public void Add(T value) { underlyingCollection.Add(value); }

        public void Clear() { underlyingCollection.Clear(); }

        public bool Contains(T value) { return underlyingCollection.Contains(value); }

        public void CopyTo(T[] array, int index)
        {
            for (var i = 0; i < underlyingCollection.Count; i++)
            {
                array[i] = converter(underlyingCollection[i]);
            }
        }

        public IEnumerator<T> GetEnumerator() { return underlyingCollection.OfType<T>().GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public int IndexOf(T value) { return underlyingCollection.IndexOf(value); }

        public void Insert(int index, T value) { underlyingCollection.Insert(index, value); }

        public bool Remove(T value)
        {
            var count = underlyingCollection.Count;
            underlyingCollection.Remove(value);
            return (count + 1) == underlyingCollection.Count;
        }

        public void RemoveAt(int index) { underlyingCollection.RemoveAt(index); }

        int ICollection<T>.Count { get { return underlyingCollection.Count; } }
        public bool IsReadOnly { get { return underlyingCollection.IsReadOnly; } }
        public bool IsFixedSize { get { return underlyingCollection.IsFixedSize; } }
        internal IList RawCollection { get { return underlyingCollection; } }

        public T this[int index]
        {
            get { return converter(underlyingCollection[index]); }
            set { underlyingCollection[index] = value; }
        }
    }
}