#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Extensions.StringComparison
{
    /// <summary>
    /// A static class containing common extension methods to <see cref="StringComparison"/> instances.
    /// </summary>
    public static class StringComparisonExtensions
    {
        /// <summary>
        /// Gets the respective <see cref="StringComparer"/> implementation corresponding to
        /// the given <paramref name="stringComparison"/> value.
        /// </summary>
        /// <param name="stringComparison">One of the <see cref="StringComparison"/> values</param>
        /// <returns>
        /// A <seealso cref="StringComparer"/> instance that corresponds to the given
        /// <paramref name="stringComparison"/> value.
        /// </returns>
        public static StringComparer GetComparer(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            System.StringComparison stringComparison)
        {
            switch (stringComparison)
            {
                case System.StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;
                case System.StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                case System.StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture;
                case System.StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase;
                #endif
                case System.StringComparison.Ordinal:
                    return StringComparer.Ordinal;
                case System.StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
                default:
                    return StringComparer.CurrentCulture;
            }
        }
    }
}
#endif