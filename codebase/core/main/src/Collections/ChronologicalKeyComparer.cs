using System;
using System.Collections.Generic;


namespace Axle.Collections
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
    internal sealed class ChronologicalKeyComparer<TKey> : IComparer<ChronologicalKey<TKey>>
    {
        public int Compare(ChronologicalKey<TKey> x, ChronologicalKey<TKey> y) => x.Timestamp.CompareTo(y.Timestamp);
    }
}