using System.Linq;
using Axle.Environment;
using Axle.Extensions.String;

namespace Axle.Text
{
    /// <summary>
    /// A static class containing the common line ending sequences;
    /// </summary>
    public static class LineEndings
    {
        public const string CRLF = "\r\n";
        public const string CR = "\r";
        public const string LF = "\n";
        public static string Current => Platform.Environment.NewLine;

        public static string[] Split(string text)
        {
            return Enumerable.ToArray(
                Enumerable.SelectMany(
                    Enumerable.SelectMany(
                        text.Split(CRLF), 
                        x => StringExtensions.Split(x, CR)), 
                    x => StringExtensions.Split(x, LF)));
        }
    }
}