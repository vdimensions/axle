#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;


namespace Axle.Collections.Generic.Extensions.EqualityComparer
{
    /// <summary>
    /// A static class to contain extension methods for the <see cref="IEqualityComparer{T}"/> type.
    /// </summary>
    public static class EqualityComparerExtensions
    {
        /// <summary>
        /// Adapts an instance of <see cref="IEqualityComparer{T1}"/> to be used as an <see cref="IEqualityComparer{T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of objects the initial comparer is capable of handling. </typeparam>
        /// <typeparam name="T2">The type of objects the adapted comparer is capable of handling. </typeparam>
        /// <param name="comparer">The initial comparer to be adapted.</param>
        /// <param name="adaptFunc">
        /// A function to convert values of type <typeparamref name="T2"/> to type <typeparamref name="T1"/>,
        /// in order for the <paramref name="comparer"/> to handle.
        /// </param>
        /// <returns>
        /// An instance of <see cref="AdaptiveEqualityComparer{T1, T2}"/>.
        /// </returns>
        public static AdaptiveEqualityComparer<T2, T1> Adapt<T1, T2>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IEqualityComparer<T1> comparer, Func<T2, T1> adaptFunc)
        {
            return new AdaptiveEqualityComparer<T2, T1>(adaptFunc, comparer);
        }
    }
}
#endif