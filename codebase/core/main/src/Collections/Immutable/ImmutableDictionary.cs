using System.Collections;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    public static class ImmutableDictionary
    {
        public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>()
        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            => new ImmutableDictionary<TKey, TValue>(System.Collections.Immutable.ImmutableDictionary.Create<TKey, TValue>());
        #else
        {
            var result = new Dictionary<TKey, TValue>();
            return new ImmutableDictionary<TKey, TValue>(result, result.Comparer);
        }
        #endif
        public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(keyComparer, nameof(keyComparer)));
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return new ImmutableDictionary<TKey, TValue>(
                System.Collections.Immutable.ImmutableDictionary.Create<TKey, TValue>(keyComparer));
            #else
            return new ImmutableDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(keyComparer), keyComparer);
            #endif
        }
        public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(
            IEqualityComparer<TKey> keyComparer,
            IEnumerable<KeyValuePair<TKey, TValue>> items)
        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            => new ImmutableDictionary<TKey, TValue>(System.Collections.Immutable.ImmutableDictionary.CreateRange(keyComparer, items));
        #else
        {
            return Create<TKey, TValue>(keyComparer).SetItems(items);
        }            
        #endif

        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> keyComparer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            Verifier.IsNotNull(Verifier.VerifyArgument(keyComparer, nameof(keyComparer)));
            return CreateRange(keyComparer, items);
        }
        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            return CreateRange(EqualityComparer<TKey>.Default, items);
        }
        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(dictionary, nameof(dictionary)));
            return CreateRange(dictionary.Comparer, dictionary);
        }
    }
    
    public class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        public static readonly ImmutableDictionary<TKey, TValue> Empty = ImmutableDictionary.Create<TKey, TValue>();
        
        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        private readonly System.Collections.Immutable.ImmutableDictionary<TKey, TValue> _impl;

        internal ImmutableDictionary(System.Collections.Immutable.ImmutableDictionary<TKey, TValue> impl)
        {
            _impl = impl;
        }
        #else
        private readonly IDictionary<TKey, TValue> _impl;
        private readonly IEqualityComparer<TKey> _keyComparer;

        internal ImmutableDictionary(IDictionary<TKey, TValue> impl, IEqualityComparer<TKey> keyComparer)
        {
            _impl = impl;
            _keyComparer = keyComparer;
        }
        #endif

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _impl.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        /// <inheritdoc />
        public bool Contains(KeyValuePair<TKey, TValue> pair) => _impl.Contains(pair);

        /// <inheritdoc />
        public bool TryGetKey(TKey equalKey, out TKey actualKey) => _impl.TryGetKey(equalKey, out actualKey);
        #endif
        
        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        /// <inheritdoc />
        #endif
        public bool ContainsKey(TKey key) => _impl.ContainsKey(key);

        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        /// <inheritdoc />
        #endif
        public bool TryGetValue(TKey key, out TValue value) => _impl.TryGetValue(key, out value);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> Clear()
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.Clear());
            #else
            return ImmutableDictionary.Create<TKey, TValue>(KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear() => Clear();

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.Add(key, value));
            #else
            return new ImmutableDictionary<TKey, TValue>(
                new Dictionary<TKey, TValue>(_impl, KeyComparer) {{key, value}}, 
                KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value) 
            => Add(key, value);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.AddRange(pairs));
            #else
            var result = new Dictionary<TKey, TValue>(_impl, KeyComparer);
            foreach (var pair in pairs)
            {
                result.Add(pair.Key, pair.Value);
            }
            return new ImmutableDictionary<TKey, TValue>(result, KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs) 
            => AddRange(pairs);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.SetItem(key, value));
            #else
            return new ImmutableDictionary<TKey, TValue>(
                new Dictionary<TKey, TValue>(_impl, KeyComparer) {[key] = value}), 
                KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value) 
            => SetItem(key, value);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.SetItems(items));
            #else
            var result = new Dictionary<TKey, TValue>(_impl, KeyComparer);
            foreach (var pair in items)
            {
                result[pair.Key] = pair.Value;
            }
            return new ImmutableDictionary<TKey, TValue>(result, KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items) 
            => SetItems(items);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.RemoveRange(keys));
            #else
            var result = new Dictionary<TKey, TValue>(_impl, KeyComparer);
            foreach (var key in keys)
            {
                result.Remove(key);
            }
            return new ImmutableDictionary<TKey, TValue>(result, KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys) 
            => RemoveRange(keys);

        /// <inheritdoc />
        public ImmutableDictionary<TKey, TValue> Remove(TKey key)
        {
            #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
            return ImmutableDictionary.CreateRange(KeyComparer, _impl.Remove(key));
            #else
            var result = new Dictionary<TKey, TValue>(_impl, KeyComparer);
            result.Remove(key);
            return new ImmutableDictionary<TKey, TValue>(result, KeyComparer);;
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
            => Remove(key);

        public TValue this[TKey key] => _impl[key];

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public IEnumerable<TKey> Keys => _impl.Keys;

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public IEnumerable<TValue> Values => _impl.Values;

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public int Count => _impl.Count;

        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        public IEqualityComparer<TKey> KeyComparer => _impl.KeyComparer;
        #else
        public IEqualityComparer<TKey> KeyComparer => _keyComparer;
        #endif
    }
}