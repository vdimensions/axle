using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// implementing the <see cref="IComparable{T}"/> interface.
    /// </summary>
    /// <seealso cref="IComparable{T}"/>
    public static class ComparableVerifier
    {
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is greater than a given value. 
        /// </summary>
        /// <typeparam name="T">The type of the argument represented by the <paramref name="argument"/> parameter</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// </param>
        /// <param name="minValue">The comparison value above which the argument will be accepted as valid. </param>
        /// <param name="message">
        /// An error message passed to the exception in case the verification fails.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="minValue"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument value does not conform to the boundary provided by the <paramref name="minValue"/> argument.
        /// </exception>
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThan<T>(this ArgumentReference<T> argument, T minValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(minValue, null))
            {
                throw new ArgumentNullException(nameof(minValue));
            }
            if (argument.Value.CompareTo(minValue) > 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is greater than a given value. 
        /// </summary>
        /// <typeparam name="T">The type of the argument represented by the <paramref name="argument"/> parameter</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// </param>
        /// <param name="minValue">The comparison value above which the argument will be accepted as valid. </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="minValue"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument value does not conform to the boundary provided by the <paramref name="minValue"/> argument.
        /// </exception>
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThan<T>(this ArgumentReference<T> argument, T minValue) where T: IComparable<T>
        {
            return IsGreaterThan(argument, minValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThanOrEqualTo<T>(this ArgumentReference<T> argument, T minValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(minValue, null))
            {
                throw new ArgumentNullException("minValue");
            }
            if (argument.Value.CompareTo(minValue) >= 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsGreaterThanOrEqualTo<T>(this ArgumentReference<T> argument, T minValue) where T: IComparable<T>
        {
            return IsGreaterThanOrEqualTo(argument, minValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThan<T>(this ArgumentReference<T> argument, T maxValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(maxValue, null))
            {
                throw new ArgumentNullException("maxValue");
            }
            if (argument.Value.CompareTo(maxValue) < 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThan<T>(this ArgumentReference<T> argument, T maxValue) where T: IComparable<T>
        {
            return IsLessThan(argument, maxValue, null);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThanOrEqualTo<T>(this ArgumentReference<T> argument, T maxValue, string message) where T: IComparable<T>
        {
            if (ReferenceEquals(maxValue, null))
            {
                throw new ArgumentNullException("maxValue");
            }
            if (argument.Value.CompareTo(maxValue) <- 0)
            {
                return argument;
            }
            throw message == null
                ? new ArgumentOutOfRangeException(argument.Name)
                : new ArgumentOutOfRangeException(argument.Name, message);
        }

        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsLessThanOrEqualTo<T>(this ArgumentReference<T> argument, T maxValue) where T: IComparable<T>
        {
            return IsLessThanOrEqualTo(argument, maxValue, null);
        }
    }
}
