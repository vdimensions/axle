using Axle.Verification;


namespace Axle.Extensions.String
{
    partial class StringExtensions
    {
        /// <summary>
        /// Retrieves the system's reference for the specified string. 
        /// </summary>
        /// <param name="str">A <see cref="string"/> to search for in the intern pool.</param>
        /// <returns>
        /// The system's reference to <paramref name="str"/>, if it is interned; otherwise, a new reference to a 
        /// <see cref="string"/> with the value of <paramref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="str"/> is <c>null</c>.
        /// </exception>
        /// <seealso cref="string.Intern(string)"/>
        public static string Intern(this string str) { return string.Intern(str.VerifyArgument(nameof(str)).IsNotNull()); }

    }
}