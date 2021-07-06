#if NETSTANDARD || NET20_OR_NEWER
using System;
using System.Diagnostics;
using Axle.Text;


namespace Axle.Verification
{
    /// <summary>
    /// Extension methods to the <see cref="ArgumentReference{T}"/> class that enable verification for arguments
    /// of type <see cref="CharSequence" />.
    /// </summary>
    /// <seealso cref="CharSequence"/>
    public static class CharSequenceVerifier
    {
        #if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static ArgumentReference<CharSequence> UncheckedIsNotEmpty(ArgumentReference<CharSequence> argument, string message)
        {
            if (argument.Value.Length == 0)
            {
                throw new ArgumentException(message ?? string.Format("Argument `{0}` cannot be an empty string.", argument.Name), argument.Name);
            }
            return argument;
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the
        /// <paramref name="argument"/> is a non-empty <see cref="CharSequence"/>.
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <param name="message">
        /// An optional exception message to be used for the generated exception in case the argument is an empty
        /// <see cref="CharSequence"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<CharSequence> IsNotEmpty(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<CharSequence> argument, string message)
        {
            return UncheckedIsNotEmpty(argument, message);
        }

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the
        /// <paramref name="argument"/> is a non-empty <see cref="CharSequence"/>.
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<CharSequence> IsNotEmpty(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<CharSequence> argument) => IsNotEmpty(argument, null);

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the
        /// <paramref name="argument"/> is not <c>null</c> or an empty <see cref="CharSequence"/>.
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<CharSequence> IsNotNullOrEmpty(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<CharSequence> argument) => IsNotEmpty(Verifier.IsNotNull(argument));

        /// <summary>
        /// Ensures the <see cref="ArgumentReference{T}">argument reference</see> represented by the
        /// <paramref name="argument"/> is not <c>null</c> or an empty <see cref="CharSequence"/>.
        /// </summary>
        /// <param name="argument">
        /// The argument that is being verified.
        /// </param>
        /// <param name="nullMessage">
        /// An optional exception message to be used for the generated exception in case the argument is <c>null</c>.
        /// </param>
        /// <param name="emptyMessage">
        /// An optional exception message to be used for the generated exception in case the argument is an empty
        /// <see cref="CharSequence"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{T}"/> instance that represents the verified argument.
        /// </returns>
        [DebuggerStepThrough]
        public static ArgumentReference<CharSequence> IsNotNullOrEmpty(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            ArgumentReference<CharSequence> argument,
            string nullMessage,
            string emptyMessage)
        {
            return UncheckedIsNotEmpty(Verifier.IsNotNull(argument, nullMessage), emptyMessage);
        }
    }
}
#endif