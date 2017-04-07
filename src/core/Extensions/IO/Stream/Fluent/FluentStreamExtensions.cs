using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.Stream.Fluent
{
    using Stream = System.IO.Stream;

    /// <summary>
    /// A static class providing extension methods to <see cref="Stream"/> instances.
    /// </summary>
    public static class FluentStreamExtensions
    {
        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be 
        /// written to the underlying device.
        /// </summary>
        /// <exception cref="System.IO.IOException">
        /// An I/O error occurs.
        /// </exception> 
        /// <param name="stream"></param>
        /// <returns>
        /// A reference to the calling stream instance.
        /// </returns>
        public static Stream FluentFlush(this Stream stream)
        {
            stream.Flush();
            return stream;
        }

        public static Stream FluentSeek(this Stream stream, long offset, SeekOrigin seekOrigin)
        {
            stream.VerifyArgument(nameof(stream)).IsNotNull().Value.Seek(offset, SeekOrigin.Begin);
            return stream;
        }
        public static Stream FluentSeek(this Stream stream, long offset, SeekOrigin seekOrigin, out long position)
        {
            position = stream.VerifyArgument(nameof(stream)).IsNotNull().Value.Seek(offset, SeekOrigin.Begin);
            return stream;
        }
    }
}
