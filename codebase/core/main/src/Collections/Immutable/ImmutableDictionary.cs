using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Collections.Immutable
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class ImmutableDictionary
    {
        public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>()
        #if NETSTANDARD
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
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(
                System.Collections.Immutable.ImmutableDictionary.Create<TKey, TValue>(keyComparer));
            #else
            return new ImmutableDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(keyComparer), keyComparer);
            #endif
        }
        public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(
            IEqualityComparer<TKey> keyComparer,
            IEnumerable<KeyValuePair<TKey, TValue>> items)
        #if NETSTANDARD
            => new ImmutableDictionary<TKey, TValue>(System.Collections.Immutable.ImmutableDictionary.CreateRange(keyComparer, items));
        #else
        {
            return Create<TKey, TValue>(keyComparer).SetItems(items);
        }            
        #endif

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> keyComparer)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(items, nameof(items)));
            Verifier.IsNotNull(Verifier.VerifyArgument(keyComparer, nameof(keyComparer)));
            return CreateRange(keyComparer, items);
        }
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
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
    
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets a reference to an empty <see cref="ImmutableDictionary{TKey,TValue}"/> instance.
        /// </summary>
        public static readonly ImmutableDictionary<TKey, TValue> Empty = ImmutableDictionary.Create<TKey, TValue>();
        
        #if NETSTANDARD
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

        #if NETSTANDARD
        /// <inheritdoc />
        public bool Contains(KeyValuePair<TKey, TValue> pair) => _impl.Contains(pair);

        /// <inheritdoc />
        public bool TryGetKey(TKey equalKey, out TKey actualKey) => _impl.TryGetKey(equalKey, out actualKey);
        #endif
        
        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public bool ContainsKey(TKey key) => _impl.ContainsKey(key);

        #if NETSTANDARD
        /// <inheritdoc />
        #endif
        public bool TryGetValue(TKey key, out TValue value) => _impl.TryGetValue(key, out value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Clear" />
        public ImmutableDictionary<TKey, TValue> Clear() => ImmutableDictionary.Create<TKey, TValue>(KeyComparer);

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear() => Clear();

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Add" />
        public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(_impl.Add(key, value));
            #else
            return new ImmutableDictionary<TKey, TValue>(
                new Dictionary<TKey, TValue>(_impl, KeyComparer) {{key, value}}, 
                KeyComparer);
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value) 
            => Add(key, value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.AddRange" />
        public ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(_impl.AddRange(pairs));
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

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItem" />
        public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            return new ImmutableDictionary<TKey, TValue>(
                #if NETSTANDARD
                _impl.SetItem(key, value)
                #else
                new Dictionary<TKey, TValue>(_impl, KeyComparer) {[key] = value}, 
                KeyComparer
                #endif
                );
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value) 
            => SetItem(key, value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItems" />
        public ImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(_impl.SetItems(items));
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

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.RemoveRange" />
        public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
        {
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(_impl.RemoveRange(keys));
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

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Remove" />
        public ImmutableDictionary<TKey, TValue> Remove(TKey key)
        {
            #if NETSTANDARD
            return new ImmutableDictionary<TKey, TValue>(_impl.Remove(key));
            #else
            var result = new Dictionary<TKey, TValue>(_impl, KeyComparer);
            result.Remove(key);
            return new ImmutableDictionary<TKey, TValue>(result, KeyComparer);;
            #endif
        }
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
            => Remove(key);

        /// <inheritdoc cref="IReadOnlyDictionary{TKey,TValue}.this" />
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

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.KeyComparer" />
        #if NETSTANDARD
        public IEqualityComparer<TKey> KeyComparer => _impl.KeyComparer;
        #else
        public IEqualityComparer<TKey> KeyComparer => _keyComparer;
        #endif
    }
}