using System;
using System.Collections.Generic;


namespace Axle.Collections.Generic.Extensions.Comparer
{
    /// <summary>
    /// A static class to contain extension mehtods for the <see cref="IComparer{T}"/> type.
    /// </summary>
    public static class ComparerExtensions
    {
        public static AdaptiveComparer<T2, T1> Adapt<T1, T2>(this IComparer<T1> comparer, Func<T2, T1> adaptFunc)
        {
            return new AdaptiveComparer<T2, T1>(adaptFunc, comparer);
        }
    }
}