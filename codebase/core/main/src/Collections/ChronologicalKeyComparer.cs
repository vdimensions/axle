#if NETSTANDARD || NET20_OR_NEWER
using System.Collections.Generic;


namespace Axle.Collections
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class ChronologicalKeyComparer<TKey> : IComparer<ChronologicalKey<TKey>>
    {
        public int Compare(ChronologicalKey<TKey> x, ChronologicalKey<TKey> y) => x.Timestamp.CompareTo(y.Timestamp);
    }
}
#endif