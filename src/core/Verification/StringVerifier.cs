using System;
using System.Diagnostics;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments 
    /// of type <see cref="string" />.
    /// </summary>
    /// <seealso cref="string"/>
    public static class StringVerifier
    {
        private static ArgumentReference<string> UncheckedIsNotEmpty(ArgumentReference<string> argument, string message)
        {
            if (argument.Value.Length == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty string.", argument.Name), argument.Name);
            }
            return argument;
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is a non-empty string. 
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <param name="message">
        /// An optional exception message to be used for the generated exception in case the argument is an empty string.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotEmpty(this ArgumentReference<string> argument, string message)
        {
            return UncheckedIsNotEmpty(Verifier.IsNotNull(argument), message);
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is a non-empty string. 
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotEmpty(this ArgumentReference<string> argument) { return IsNotEmpty(argument, null); }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not <c>null</c> or an empty string. 
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotNullOrEmpty(this ArgumentReference<string> argument)
        {
            return IsNotEmpty(Verifier.IsNotNull(argument));
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the <paramref name="argument"/>
        /// is not <c>null</c> or an empty string. 
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <param name="nullMessage">
        /// An optional exception message to be used for the generated exception in case the argument is <c>null</c>.
        /// </param>
        /// <param name="emptyMessage">
        /// An optional exception message to be used for the generated exception in case the argument is an empty string.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<string> IsNotNullOrEmpty(
            this ArgumentReference<string> argument, 
            string nullMessage, 
            string emptyMessage)
        {
            return UncheckedIsNotEmpty(Verifier.IsNotNull(argument, nullMessage), emptyMessage);
        }
    }
}
