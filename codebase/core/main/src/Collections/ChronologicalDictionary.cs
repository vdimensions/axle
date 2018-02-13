using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.Collections.Sdk;


namespace Axle.Collections
{
    /// <summary>
    /// Represents a collection of key/value pairs that are sorted by the time of insertion (chronologically)
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary. </typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <remarks>This class cannot be inherited.</remarks>
    public sealed partial class ChronologicalDictionary<TKey, TValue> : DictionaryProxy<TKey, TValue>
    {
        private sealed partial class TimestampDictionary : Dictionary<ChronologicalKey<TKey>, TValue>, IDictionary<TKey, TValue>
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            #if !NETSTANDARD
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

            private IEnumerable<KeyValuePair<TKey, TValue>> Enumerate() => _collection
                                                                           .OrderBy(x => x.Key, new ChronologicalKeyComparer<TKey>())
                                                                           .Select(x => new KeyValuePair<TKey, TValue>(x.Key.Key, x.Value));

            #region Implementation of IEnumerable<KeyValuePair<TKey,TValue>>
            IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return Enumerate().GetEnumerator(); }
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
                base.Keys.Select(x => new KeyValuePair<TKey, TValue>(x.Key, this[x])).ToList().CopyTo(array, arrayIndex);
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

            new private ICollection<TKey> Keys { get { return Enumerate().Select(key => key.Key).ToArray(); } }
            ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;

            new private ICollection<TValue> Values => Enumerate().Select(x => x.Value).ToArray();
            ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;
            #endregion
        }

        public ChronologicalDictionary() : base(new TimestampDictionary()) { }
        public ChronologicalDictionary(IEqualityComparer<TKey> comparer) : base(new TimestampDictionary(comparer)) { }
        public ChronologicalDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(new TimestampDictionary(capacity, comparer)) { }
        public ChronologicalDictionary(int capacity) : base(new TimestampDictionary(capacity)) { }
    }
}