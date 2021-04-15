using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Extensions.Tuple
{
    /// <summary>
    /// A static class to contain extension methods for tuples.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class PrependExtensions
    {
        /// <summary>
        /// Produces a 3-tuple, or triple, by prepending the supplied <paramref name="value"/> to the current
        /// 2-<paramref name="tuple"/> instance. 
        /// </summary>
        /// <param name="tuple">
        /// The 2-tuple (or pair) to prepend the value to.
        /// </param>
        /// <param name="value">
        /// The value to be the first item in the resulting 3-tuple.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to prepend.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of the first item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// he type of the second item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <returns>
        /// A 3-tuple, or triple, produced by prepending the supplied <paramref name="value"/> to the current
        /// 2-<paramref name="tuple"/> instance. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="tuple"/> is <c>null</c>.
        /// </exception>
        public static System.Tuple<T, T1, T2> Prepend<T, T1, T2>(this System.Tuple<T1, T2> tuple, T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(tuple, nameof(tuple)));
            return System.Tuple.Create(value, tuple.Item1, tuple.Item2);
        }
        
        /// <summary>
        /// Produces a 4-tuple, or quadruple, by prepending the supplied <paramref name="value"/> to the current
        /// 3-<paramref name="tuple"/> instance. 
        /// </summary>
        /// <param name="tuple">
        /// The 3-tuple (or triple) to prepend the value to.
        /// </param>
        /// <param name="value">
        /// The value to be the first item in the resulting 4-tuple.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to prepend.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of the first item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// he type of the second item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// he type of the third item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <returns>
        /// A 4-tuple, or triple, produced by prepending the supplied <paramref name="value"/> to the current
        /// 3-<paramref name="tuple"/> instance. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="tuple"/> is <c>null</c>.
        /// </exception>
        public static System.Tuple<T, T1, T2, T3> Prepend<T, T1, T2, T3>(this System.Tuple<T1, T2, T3> tuple, T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(tuple, nameof(tuple)));
            return System.Tuple.Create(value, tuple.Item1, tuple.Item2, tuple.Item3);
        }
        
        /// <summary>
        /// Produces a 5-tuple by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </summary>
        /// <param name="tuple">
        /// The 4-tuple to prepend the value to.
        /// </param>
        /// <param name="value">
        /// The value to be the first item in the resulting 5-tuple.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to prepend.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of the first item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// he type of the second item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// he type of the third item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T4">
        /// he type of the fourth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <returns>
        /// A 5-tuple, produced by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="tuple"/> is <c>null</c>.
        /// </exception>
        public static System.Tuple<T, T1, T2, T3, T4> Prepend<T, T1, T2, T3, T4>(
            this System.Tuple<T1, T2, T3, T4> tuple, 
            T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(tuple, nameof(tuple)));
            return System.Tuple.Create(value, tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }
        
        /// <summary>
        /// Produces a 6-tuple by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </summary>
        /// <param name="tuple">
        /// The 5-tuple to prepend the value to.
        /// </param>
        /// <param name="value">
        /// The value to be the first item in the resulting 6-tuple.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to prepend.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of the first item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// he type of the second item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// he type of the third item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T4">
        /// he type of the fourth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T5">
        /// he type of the fifth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <returns>
        /// A 6-tuple, produced by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="tuple"/> is <c>null</c>.
        /// </exception>
        public static System.Tuple<T, T1, T2, T3, T4, T5> Prepend<T, T1, T2, T3, T4, T5>(
            this System.Tuple<T1, T2, T3, T4, T5> tuple, 
            T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(tuple, nameof(tuple)));
            return System.Tuple.Create(
                value, 
                tuple.Item1, 
                tuple.Item2, 
                tuple.Item3, 
                tuple.Item4,
                tuple.Item5);
        }
        
        /// <summary>
        /// Produces a 7-tuple by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </summary>
        /// <param name="tuple">
        /// The 6-tuple to prepend the value to.
        /// </param>
        /// <param name="value">
        /// The value to be the first item in the resulting 7-tuple.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to prepend.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of the first item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// he type of the second item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T3">
        /// he type of the third item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T4">
        /// he type of the fourth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T5">
        /// he type of the fifth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <typeparam name="T6">
        /// he type of the sixth item in the current <paramref name="tuple"/>.
        /// </typeparam>
        /// <returns>
        /// A 7-tuple, produced by prepending the supplied <paramref name="value"/> to the current
        /// <paramref name="tuple"/> instance. 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="tuple"/> is <c>null</c>.
        /// </exception>
        public static System.Tuple<T, T1, T2, T3, T4, T5, T6> Prepend<T, T1, T2, T3, T4, T5, T6>(
            this System.Tuple<T1, T2, T3, T4, T5, T6> tuple, 
            T value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(tuple, nameof(tuple)));
            return System.Tuple.Create(
                value, 
                tuple.Item1, 
                tuple.Item2, 
                tuple.Item3, 
                tuple.Item4,
                tuple.Item5,
                tuple.Item6);
        }
    }
}
#endif