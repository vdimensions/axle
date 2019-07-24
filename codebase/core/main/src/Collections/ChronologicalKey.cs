using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Collections
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal struct ChronologicalKey<TKey> : IEquatable<ChronologicalKey<TKey>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TKey _key;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly long _timestamp;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEqualityComparer<TKey> _comparer;

        public ChronologicalKey(TKey key, DateTime dateTime, IEqualityComparer<TKey> comparer) : this()
        {
            _key = key;
            _comparer = comparer;
            _timestamp = dateTime.Ticks;
        }

        public override int GetHashCode() => _key.GetHashCode();
        public override bool Equals(object obj) => obj is ChronologicalKey<TKey> ck && Equals(ck);
        public bool Equals(ChronologicalKey<TKey> other) => _comparer.Equals(Key, other.Key);

        public TKey Key => _key;
        public long Timestamp => _timestamp;
    }
}
