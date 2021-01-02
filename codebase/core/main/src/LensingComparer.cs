#if NETSTANDARD || NET20_OR_NEWER || UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle
{
    /// <summary>
    /// An implementation of the <see cref="IComparer{T1}"/> interface, that can compare instances of 
    /// <typeparamref name="T1"/>. The comparer works by delegating the comparison logic to another 
    /// <see cref="IComparer{T2}"/> implementation that can handle instances of <typeparamref name="T2"/>.
    /// The delegation is aided by invoking a user-defined "lensing" function which maps the source 
    /// comparable object (of type <typeparamref name="T1"/>) to an instance of the <typeparamref name="T2"/>
    /// type, before passing it to the other comparer.
    /// Usually <typeparamref name="T2"/> is the type of a member of <typeparamref name="T1"/> and the lensing function
    /// exposes the respective member for use by the nested comparer.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of objects the current <see cref="IComparer{T}"/> can handle.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of objects that the underlying <see cref="IComparer{T}"/> can handle.
    /// </typeparam>
    /// <seealso cref="LensingEqualityComparer{T1,T2}"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public class LensingComparer<T1, T2> : IComparer<T1>
    {
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly Func<T1, T2> _lensFunc;
        #if !DEBUG
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        #endif
        private readonly IComparer<T2> _actualComparer;

        /// <summary>
        /// Creates a new instance of the <see cref="LensingComparer{T1,T2}"/> class with the
        /// provided <paramref name="lensFunction"/> and <paramref name="comparer"/>.
        /// </summary>
        /// <param name="lensFunction">
        /// A <see cref="Func{T, TResult}"/> that is used to map values of <typeparamref name="T1"/>
        /// to <see langword="abstract"/> value of <typeparamref name="T2"/>.
        /// </param>
        /// <param name="comparer">
        /// A <see cref="IComparer{T}"/>, which handles the converted by the <paramref name="lensFunction"/> 
        /// values of <typeparamref name="T2"/>, and determines the comparison result returned by the current
        /// <see cref="LensingComparer{T1,T2}"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="lensFunction"/> or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        public LensingComparer(Func<T1, T2> lensFunction, IComparer<T2> comparer)
        {
            _lensFunc = Verifier.IsNotNull(Verifier.VerifyArgument(lensFunction, nameof(lensFunction)));
            _actualComparer = Verifier.IsNotNull(Verifier.VerifyArgument(comparer, nameof(comparer))).Value;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="LensingComparer{T1,T2}"/> class with the
        /// provided <paramref name="lensFunction"/> and the default comparer for <typeparamref name="T2"/>.
        /// </summary>
        /// <param name="lensFunction">
        /// A <see cref="Func{T, TResult}"/> that is used to map values of <typeparamref name="T1"/>
        /// to <see langword="abstract"/> value of <typeparamref name="T2"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lensFunction"/> is <c>null</c>.
        /// </exception>
        public LensingComparer(Func<T1, T2> lensFunction) : this(lensFunction, Comparer<T2>.Default){}

        /// <inheritdoc/>
        public int Compare(T1 x, T1 y) => _actualComparer.Compare(_lensFunc(x), _lensFunc(y));
    }
}
#endif