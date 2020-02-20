#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle
{
    /// <summary>
    /// An implementation of the <see cref="IComparer{T1}"/> interface, that can compare instances of 
    /// <typeparamref name="T1"/>. The comparer works by delegating the comparison logic to another 
    /// <see cref="IComparer{T2}"/> implementation that can handle instances of <typeparamref name="T2"/>. 
    /// The delegation is aided by invoking a user-defined adaptation function which maps the source 
    /// comparable object (of type <typeparamref name="T1"/>) to an instance of the <typeparamref name="T2"/>
    /// type, before passing it to the other comparer.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of objects the current <see cref="IComparer{T}"/> can handle.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of objects that the underlying <see cref="IComparer{T}"/> can handle.
    /// </typeparam>
    /// <seealso cref="AdaptiveEqualityComparer{T1, T2}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public class AdaptiveComparer<T1, T2> : IComparer<T1>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly Func<T1, T2> _adaptFunc;
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IComparer<T2> _actualComparer;

        /// <summary>
        /// Creates a new instance of the <see cref="AdaptiveComparer{T1, T2}"/> class with the
        /// provided <paramref name="adaptationFunction"/> and <paramref name="comparer"/>.
        /// </summary>
        /// <param name="adaptationFunction">
        /// A <see cref="Func{T, TResult}"/> that is used to map values of <typeparamref name="T1"/>
        /// to <see langword="abstract"/> value of <typeparamref name="T2"/>.
        /// </param>
        /// <param name="comparer">
        /// A <see cref="IComparer{T}"/>, which handles the converted by the <paramref name="adaptationFunction"/> 
        /// values of <typeparamref name="T2"/>, and determines the comparison result returned by the current
        /// <see cref="AdaptiveComparer{T1, T2}"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="adaptationFunction"/> or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        public AdaptiveComparer(Func<T1, T2> adaptationFunction, IComparer<T2> comparer)
        {
            _adaptFunc = Verifier.IsNotNull(Verifier.VerifyArgument(adaptationFunction, nameof(adaptationFunction)));
            _actualComparer = Verifier.IsNotNull(Verifier.VerifyArgument(comparer, nameof(comparer))).Value;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="AdaptiveComparer{T1, T2}"/> class with the
        /// provided <paramref name="adaptationFunction"/> and the default comparer for <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="adaptationFunction">
        /// A <see cref="Func{T, TResult}"/> that is used to map values of <typeparamref name="T1"/>
        /// to <see langword="abstract"/> value of <typeparamref name="T2"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="adaptationFunction"/> is <c>null</c>.
        /// </exception>
        public AdaptiveComparer(Func<T1, T2> adaptationFunction) : this(adaptationFunction, Comparer<T2>.Default){}

        /// <inheritdoc/>
        public int Compare(T1 x, T1 y) => _actualComparer.Compare(_adaptFunc(x), _adaptFunc(y));
    }
}
#endif