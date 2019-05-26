#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;


namespace Axle.Collections.Generic.Extensions.Comparer
{
    /// <summary>
    /// A static class to contain extension methods for the <see cref="IComparer{T}"/> type.
    /// </summary>
    public static class ComparerExtensions
    {
        /// <summary>
        /// Adapts an instance of <see cref="IComparer{T1}"/> to be used as an <see cref="IComparer{T2}"/>.
        /// </summary>
        /// <typeparam name="T1">The type of objects the initial comparer is capable of handling. </typeparam>
        /// <typeparam name="T2">The type of objects the adapted comparer is capable of handling. </typeparam>
        /// <param name="comparer">The initial comparer to be adapted.</param>
        /// <param name="adaptFunc">
        /// A function to convert values of type <typeparamref name="T2"/> to type <typeparamref name="T1"/>,
        /// in order for the <paramref name="comparer"/> to handle.
        /// </param>
        /// <returns>
        /// An instance of <see cref="AdaptiveComparer{T1, T2}"/>.
        /// </returns>
        public static AdaptiveComparer<T2, T1> Adapt<T1, T2>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IComparer<T1> comparer, Func<T2, T1> adaptFunc) => new AdaptiveComparer<T2, T1>(adaptFunc, comparer);
    }
}
#endif