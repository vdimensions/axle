using System;
using System.Diagnostics;


namespace Axle.Collections
{
    #if !NETSTANDARD
    [Serializable]
    #endif
    internal struct ChronologicalKey<TKey> : IEquatable<ChronologicalKey<TKey>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TKey _key;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly long _timestamp;

        public ChronologicalKey(TKey key, DateTime dateTime) : this()
        {
            _key = key;
            _timestamp = dateTime.Ticks;
        }

        public override int GetHashCode() => _key.GetHashCode();
        public bool Equals(ChronologicalKey<TKey> other) => Equals(Key, other.Key);

        public TKey Key => _key;
        public long Timestamp => _timestamp;
    }
}