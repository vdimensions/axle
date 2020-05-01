using System.Linq;
using Axle.Environment;
using Axle.Extensions.String;

namespace Axle.Text
{
    /// <summary>
    /// A static class containing the common line ending sequences.
    /// </summary>
    public static class LineEndings
    {
        /// <summary>
        /// Gets the line ending character sequence that is common for the <see cref="OperatingSystemID.Windows"/>
        /// platforms. 
        /// </summary>
        public const string CRLF = "\r\n";
        /// <summary>
        /// Gets the line ending character sequence that is common for the <see cref="OperatingSystemID.Mac"/>
        /// platforms. 
        /// </summary>
        public const string CR = "\r";
        /// <summary>
        /// Gets the line ending character sequence that is common for the <see cref="OperatingSystemID.Unix"/>
        /// platforms. 
        /// </summary>
        public const string LF = "\n";
        /// <summary>
        /// Gets the line ending character sequence for the current platform. 
        /// </summary>
        public static string Current => Platform.Environment.NewLine;

        /// <summary>
        /// Returns a string array that contains the lines of a <paramref name="text"/>, delimited by the different line
        /// endings represented by the members of the <see cref="LineEndings"/> class.
        /// <remarks>
        /// The separation is performed by first separating the text by the <see cref="CRLF"/> sequence, and then each
        /// piece is further being split by the <see cref="CR"/> and <see cref="LF"/> symbols.
        /// This will deliver equal results regardless of the type of platform the calling code is running on,
        /// and will not produce unnecessary blank lines.
        /// </remarks>
        /// </summary>
        /// <param name="text">
        /// The text to split into multiple lines.
        /// </param>
        /// <returns>
        /// A string array that contains the lines of a <paramref name="text"/>, delimited by the different line endings
        /// represented by the members of the <see cref="LineEndings"/> class.
        /// </returns>
        public static string[] Split(string text)
        {
            return Enumerable.ToArray(
                Enumerable.SelectMany(
                    Enumerable.SelectMany(
                        StringExtensions.Split(text, CRLF), 
                        x => StringExtensions.Split(x, CR)),
                    x => StringExtensions.Split(x, LF)));
        }
    }
}