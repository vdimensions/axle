using System;

namespace Axle.Verification
{
    /// <summary>
    /// A static class to contain validation methods for arguments of type <see cref="Uri"/>.
    /// </summary>
    public static class UriVerifier
    {
        /// <summary>
        /// Checks if the provided <paramref name="argument"/> represents an absolute URI.
        /// </summary>
        /// <param name="argument">
        /// The argument to verify.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided <paramref name="argument"/> does not represent an absolute uri.
        /// </exception>
        /// <seealso cref="Uri.IsAbsoluteUri"/>
        public static ArgumentReference<Uri> IsAbsoluteUri(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            ArgumentReference<Uri> argument)
        {
            if (argument.Value.IsAbsoluteUri)
            {
                return argument;
            }
            throw new ArgumentException("The provided value must be an absolute uri. ", argument.Name);
        }
        /// <summary>
        /// Checks if the provided <paramref name="argument"/> represents a relative URI.
        /// </summary>
        /// <param name="argument">
        /// The argument to verify.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided <paramref name="argument"/> does not represent an relative uri.
        /// </exception>
        /// <seealso cref="Uri.IsAbsoluteUri"/>
        public static ArgumentReference<Uri> IsRelativeUri(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            ArgumentReference<Uri> argument)
        {
            if (!argument.Value.IsAbsoluteUri)
            {
                return argument;
            }
            throw new ArgumentException("The provided value must be a relative uri. ", argument.Name);
        }

        /// <summary>
        /// Checks if the provided <paramref name="argument"/> represents a filesystem location.
        /// </summary>
        /// <param name="argument">
        /// The argument to verify.
        /// </param>
        /// <returns>
        /// The <see cref="ArgumentReference{Type}"/> instance that represents the verified argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided <paramref name="argument"/> does not represent a filesystem location.
        /// </exception>
        /// <seealso cref="Uri.IsFile"/>
        public static ArgumentReference<Uri> IsFile(
            #if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
            this
            #endif
            ArgumentReference<Uri> argument)
        {
            if (argument.Value.IsFile)
            {
                return argument;
            }
            throw new ArgumentException("The provided value must be a filesystem location. ", argument.Name);
        }
    }
}
