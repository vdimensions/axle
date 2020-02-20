#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments
    /// of array types.
    /// </summary>
    public static class ArrayVerifier
    {
        /// <summary>
        /// Ensures the specified array argument is not an empty array.
        /// </summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type
        /// <typeparamref name="T"/> which is being verified.
        /// </param>
        /// <param name="message">
        /// An optional error message to be passed to the exception in case the given argument is an empty array.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the argument being verified.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<T[]> IsNotEmpty<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<T[]> argument,
            string message)
        {
            if (argument.Value.Length == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty array.", argument.Name), argument.Name);
            }
            return argument;
        }

        /// <summary>
        /// Ensures the specified array argument is not an empty array.
        /// </summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type
        /// <typeparamref name="T"/> which is being verified.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the argument being verified.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<T[]> IsNotEmpty<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<T[]> argument) => IsNotEmpty(argument, null);
    }
}
#endif