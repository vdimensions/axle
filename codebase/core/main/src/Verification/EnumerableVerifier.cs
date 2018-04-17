using System;
using System.Collections;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="IEnumerable" />.
    /// </summary>
    /// <seealso cref="IEnumerable"/>
    public static class EnumerableVerifier
    {
        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection to be verified.</typeparam>
        /// <param name="argument">
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </param>
        /// <param name="message">
        /// An optional error message to use for the thrown exception in case the validation fails.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument, same as <paramref name="argument"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the verified <paramref name="argument"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the verified <paramref name="argument"/> represents an empty collection.
        /// </exception>
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNullOrEmpty<T>(this ArgumentReference<T> argument, string message) where T: IEnumerable
        {
            var e = argument.IsNotNull().Value.GetEnumerator();
            try
            {
                if (!e.MoveNext())
                {
                    throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty collection.", argument.Name), argument.Name);
                }
                return argument;
            }
            finally
            {
                if (e is IDisposable d)
                {
                    d.Dispose();
                }
            }
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection to be verified.</typeparam>
        /// <param name="argument">
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument, same as <paramref name="argument"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the verified <paramref name="argument"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the verified <paramref name="argument"/> represents an empty collection.
        /// </exception>
        [DebuggerStepThrough]
        public static ArgumentReference<T> IsNotNullOrEmpty<T>(this ArgumentReference<T> argument) where T: IEnumerable
        {
            return IsNotNullOrEmpty(argument, null);
        }
    }
}
