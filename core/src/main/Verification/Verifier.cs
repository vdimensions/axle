using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Axle.Verification
{
    public static class Verifier
    {
        /// <summary>
        /// Creates an instance of <see cref="ArgumentReference{T}"/> representing the passed object as an argument 
        /// to a method or constructor.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">
        /// The argument value.
        /// </param>
        /// <param name="argumentName">
        /// The name of the argument to be represented by the produced <see cref="ArgumentReference{T}"/> instance.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ArgumentReference{T}"/> representing the passed object as an argument 
        /// to a method or constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argumentName"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="ArgumentReference{T}"/>
        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> VerifyArgument<T>(
            this T argument,
            #if !NETSTANDARD
            [System.ComponentModel.Localizable(false)]
            #endif
            string argumentName)
        {
            if (argumentName == null)
            {
                throw new ArgumentNullException(nameof(argumentName));
            }
            return new ArgumentReference<T>(argumentName, argument);
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not <c>null</c>. In case the verification fails, an <see cref="ArgumentNullException"/> is thrown. 
        /// </summary>
        /// <typeparam name="T">The type of the argument represented by the <paramref name="argument"/> parameter</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// </param>
        /// <param name="message">
        /// Custom exception message to be used in case the argument validation fails.
        /// </param>
        /// <exception cref="ArgumentNullException">The represented by the <paramref name="argument"/> parameter object is <c>null</c></exception>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNull<T>(this ArgumentReference<T> argument, string message)
        {
            if (ReferenceEquals(argument.Value, null))
            {
                throw message == null ? new ArgumentNullException(argument.Name) : new ArgumentNullException(argument.Name, message);
            }
            return argument;
        }
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not <c>null</c>. In case the verification fails, an <see cref="ArgumentNullException"/> is thrown. 
        /// </summary>
        /// <typeparam name="T">The type of the argument represented by the <paramref name="argument"/> parameter</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// </param>
        /// <exception cref="ArgumentNullException">The represented by the <paramref name="argument"/> parameter object is <c>null</c></exception>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNull<T>(this ArgumentReference<T> argument)
        {
            return IsNotNull(argument, null);
        }

        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNot<T>(this ArgumentReference<T> argument, T value, string message)
        {
            return IsTrue(argument, x => x.Equals(value), message);
        }
        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNot<T>(this ArgumentReference<T> argument, T value, IEqualityComparer<T> comparer, string message)
        {
            return IsTrue(argument, x => comparer.Equals(x, value), message);
        }

        #if NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsTrue<T>(this ArgumentReference<T> argument, Predicate<T> condition, string message)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (!condition(argument.Value))
            {
                throw new ArgumentException(argument.Name, message);
            }
            return argument;
        }
    }
}
