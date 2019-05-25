#if NETSTANDARD || NET20_OR_NEWER
using System.Collections;
using System.Collections.Generic;


namespace Axle.Collections.Sdk
{
    /// <summary>
    /// An abstract base class that can be used as a wrapper over an <see cref="IDictionary{TKey, TValue}"/> instance.
    /// Can be used as a base class for dictionary-based key/value collections. The initial implementation delegates all
    /// <see cref="IDictionary{TKey, TValue}"/> logic to the provided inner dictionary class.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary. </typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public abstract class DictionaryDecorator<TKey, TValue> : IDictionary<TKey, TValue>
    {
        protected readonly IDictionary<TKey, TValue> Target;

        protected DictionaryDecorator(IDictionary<TKey, TValue> target)
        {
            Target = target;
        }

        #region Implementation of IEnumerable
        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Target.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item);
        protected virtual void Add(KeyValuePair<TKey, TValue> item) => Target.Add(item);

        public virtual void Clear() => Target.Clear();

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => Contains(item);
        protected virtual bool Contains(KeyValuePair<TKey, TValue> item) => Target.Contains(item);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => CopyTo(array, arrayIndex);
        protected virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Target.CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item);
        protected virtual bool Remove(KeyValuePair<TKey, TValue> item) => Target.Remove(item);

        public virtual int Count => Target.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => Target.IsReadOnly;
        protected virtual bool IsReadOnly => Target.IsReadOnly;
        #endregion

        #region Implementation of IDictionary<TKey,TValue>
        /// <inheritdoc />
        public virtual bool ContainsKey(TKey key) => Target.ContainsKey(key);

        /// <inheritdoc />
        public virtual void Add(TKey key, TValue value) => Target.Add(key, value);

        /// <inheritdoc />
        public virtual bool Remove(TKey key) => Target.Remove(key);

        /// <inheritdoc />
        public virtual bool TryGetValue(TKey key, out TValue value) => Target.TryGetValue(key, out value);

        /// <inheritdoc />
        public virtual TValue this[TKey key]
        {
            get => Target[key];
            set => Target[key] = value;
        }

        /// <inheritdoc />
        public virtual ICollection<TKey> Keys => Target.Keys;

        /// <inheritdoc />
        public virtual ICollection<TValue> Values => Target.Values;
        #endregion
    }
}
#endif