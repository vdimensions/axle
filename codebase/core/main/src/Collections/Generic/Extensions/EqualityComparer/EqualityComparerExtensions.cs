using System;
using System.Collections.Generic;


namespace Axle.Collections.Generic.Extensions.EqualityComparer
{
    /// <summary>
    /// A static class to contain extension mehtods for the <see cref="IEqualityComparer{T}"/> type.
    /// </summary>
    public static class EqualityComparerExtensions
    {
        public static AdaptiveEqualityComparer<T2, T1> Adapt<T1, T2>(this IEqualityComparer<T1> comparer, Func<T2, T1> adaptFunc)
        {
            return new AdaptiveEqualityComparer<T2, T1>(adaptFunc, comparer);
        }
    }
}