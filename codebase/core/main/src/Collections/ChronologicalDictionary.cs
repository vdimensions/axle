#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Axle.Collections.Sdk;


namespace Axle.Collections
{
    /// <summary>
    /// Represents a collection of key/value pairs that are sorted by the time of insertion (chronologically)
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary. </typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <remarks>This class cannot be inherited.</remarks>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ChronologicalDictionary<TKey, TValue> : DictionaryDecorator<TKey, TValue>
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [Serializable]
        private sealed class TimestampDictionary : Dictionary<ChronologicalKey<TKey>, TValue>, IDictionary<TKey, TValue>, ISerializable
        #else
        private sealed class TimestampDictionary : Dictionary<ChronologicalKey<TKey>, TValue>, IDictionary<TKey, TValue>
        #endif
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            [NonSerialized]
            #endif
            private readonly ICollection<KeyValuePair<ChronologicalKey<TKey>, TValue>> _collection;

            private TimestampDictionary(IDictionary<ChronologicalKey<TKey>, TValue> dictionary) : base(dictionary)
            {
                _collection = this;
            }
            public TimestampDictionary() : this(
                    new Dictionary<ChronologicalKey<TKey>, TValue>(new AdaptiveEqualityComparer<ChronologicalKey<TKey>, TKey>(x => x.Key))) { }
            public TimestampDictionary(int capacity) : this(
                    new Dictionary<ChronologicalKey<TKey>, TValue>(capacity, new AdaptiveEqualityComparer<ChronologicalKey<TKey>, TKey>(x => x.Key))) { }
            public TimestampDictionary(int capacity, IEqualityComparer<TKey> comparer) : this(
                    new Dictionary<ChronologicalKey<TKey>, TValue>(capacity, new AdaptiveEqualityComparer<ChronologicalKey<TKey>, TKey>(x => x.Key, comparer))) { }
            public TimestampDictionary(IEqualityComparer<TKey> comparer) : this(
                    new Dictionary<ChronologicalKey<TKey>, TValue>(new AdaptiveEqualityComparer<ChronologicalKey<TKey>, TKey>(x => x.Key, comparer))) { }
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            internal TimestampDictionary(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
            {
                _collection = this;
            }

            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => GetObjectData(info, context);
            #endif

            private IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
            {
                return Enumerable.Select(
                    Enumerable.OrderBy(_collection, x => x.Key, new ChronologicalKeyComparer<TKey>()), x => new KeyValuePair<TKey, TValue>(x.Key.Key, x.Value));
            }

            #region Implementation of IEnumerable<KeyValuePair<TKey,TValue>>
            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => Enumerate().GetEnumerator();
            #endregion

            #region Implementation of ICollection<KeyValuePair<TKey,TValue>>
            void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
            {
                var k = new ChronologicalKey<TKey>(item.Key, DateTime.UtcNow);
                var kvp = new KeyValuePair<ChronologicalKey<TKey>, TValue>(k, item.Value);
                _collection.Add(kvp);
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
            {
                var k = new ChronologicalKey<TKey>(item.Key, DateTime.UtcNow);
                var kvp = new KeyValuePair<ChronologicalKey<TKey>, TValue>(k, item.Value);
                return _collection.Contains(kvp);
            }

            void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            {
                #if NETSTANDARD || NET35_OR_NEWER
                base.Keys.Select(x => new KeyValuePair<TKey, TValue>(x.Key, this[x])).ToList().CopyTo(array, arrayIndex);
                #else
                using (var enumerator = Enumerate().GetEnumerator())
                for (var i = arrayIndex; i < array.Length && enumerator.MoveNext(); i++)
                {
                    array[i] = enumerator.Current;
                }
                #endif
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
            {
                var k = new ChronologicalKey<TKey>(item.Key, DateTime.UtcNow);
                var kvp = new KeyValuePair<ChronologicalKey<TKey>, TValue>(k, item.Value);
                return _collection.Remove(kvp);
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => _collection.IsReadOnly;
            #endregion

            #region Implementation of IDictionary<TKey,TValue>
            public bool ContainsKey(TKey key) => ContainsKey(new ChronologicalKey<TKey>(key, DateTime.UtcNow));

            public void Add(TKey key, TValue value) => Add(new ChronologicalKey<TKey>(key, DateTime.UtcNow), value);

            public bool Remove(TKey key) => Remove(new ChronologicalKey<TKey>(key, DateTime.UtcNow));

            public bool TryGetValue(TKey key, out TValue value) => TryGetValue(new ChronologicalKey<TKey>(key, DateTime.UtcNow), out value);

            public TValue this[TKey key]
            {
                get => base[new ChronologicalKey<TKey>(key, DateTime.UtcNow)];
                set
                {
                    var k = new ChronologicalKey<TKey>(key, DateTime.UtcNow);
                    Remove(k);
                    base[k] = value;
                }
            }

            new private ICollection<TKey> Keys => Enumerable.ToArray(Enumerable.Select(Enumerate(), key => key.Key));
            ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;

            new private ICollection<TValue> Values => Enumerable.ToArray(Enumerable.Select(Enumerate(), key => key.Value));
            ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;
            #endregion
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ChronologicalDictionary{TKey,TValue}"/> class.
        /// </summary>
        public ChronologicalDictionary() : base(new TimestampDictionary()) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ChronologicalDictionary{TKey,TValue}"/> class using the provided
        /// <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">
        /// An instance of <see cref="IEqualityComparer{TKey}"/> to be used for key comparison.
        /// </param>
        public ChronologicalDictionary(IEqualityComparer<TKey> comparer) : base(new TimestampDictionary(comparer)) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ChronologicalDictionary{TKey,TValue}"/> class
        /// with the specified <paramref name="capacity"/> and using the provided <paramref name="comparer"/>.
        /// </summary>
        /// <param name="capacity">
        /// The initial capacity of the underlying collection.
        /// </param>
        /// <param name="comparer">
        /// An instance of <see cref="IEqualityComparer{TKey}"/> to be used for key comparison.
        /// </param>
        public ChronologicalDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(new TimestampDictionary(capacity, comparer)) { }
        /// <summary>
        /// Creates a new instance of the <see cref="ChronologicalDictionary{TKey,TValue}"/> class
        /// with the specified <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">
        /// The initial capacity of the underlying collection.
        /// </param>
        public ChronologicalDictionary(int capacity) : base(new TimestampDictionary(capacity)) { }
    }
}
#endif