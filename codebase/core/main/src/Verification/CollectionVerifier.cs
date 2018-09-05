#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="ICollection" />.
    /// </summary>
    /// <seealso cref="ICollection"/>
    public static class CollectionVerifier
    {
        /// <summary>
        /// Ensures the specified argument is not an empty collection.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// which is being verified. 
        /// </param>
        /// <param name="message">
        /// An optional error message to be passed to the exception in case the given argument is an empty collection.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the argument being verified.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotEmpty<T>(this ArgumentReference<T> argument, string message) where T:ICollection
        {
            if (argument.Value.Count == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty collection.", argument.Name), argument.Name);
            }
            return argument;
        }

        /// <summary>
        /// Ensures the specified argument is not an empty collection.
        /// </summary>
        /// <param name="argument">
        /// An instance of <see cref="ArgumentReference{T}"/> that represents a method/constructor argument of type <typeparamref name="T"/>
        /// which is being verified. 
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the argument being verified.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotEmpty<T>(this ArgumentReference<T> argument) where T:ICollection { return IsNotEmpty(argument, null); }
    }
}
#endif