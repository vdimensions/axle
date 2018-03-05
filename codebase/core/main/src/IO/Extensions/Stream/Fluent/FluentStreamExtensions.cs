using System.IO;

using Axle.Verification;


namespace Axle.IO.Extensions.Stream.Fluent
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
        /// <param name="stream">
        /// The target <see cref="Stream"/> instance this extension method is being called on.
        /// </param>
        /// <returns>
        /// A reference to the target <see cref="Stream"/> instance this extension method is being called on.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception> 
        public static Stream FluentFlush(this Stream stream)
        {
            stream.VerifyArgument(nameof(stream)).IsNotNull().Value.Flush();
            return stream;
        }
        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="stream">
        /// The target <see cref="Stream"/> instance this extension method is being called on.
        /// </param>
        /// <param name="offset">
        /// A byte offset relative to the origin parameter. 
        /// </param>
        /// <param name="seekOrigin">
        /// A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.
        /// </param>
        /// <returns>
        /// A reference to the target <see cref="Stream"/> instance this extension method is being called on.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="stream" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurs.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The stream does not support seeking, such as if the stream is constructed from a pipe or console output.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the stream was closed.
        /// </exception>
        public static Stream FluentSeek(this Stream stream, long offset, SeekOrigin seekOrigin)
        {
            stream.VerifyArgument(nameof(stream)).IsNotNull().Value.Seek(offset, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="stream">
        /// The target <see cref="Stream"/> instance this extension method is being called on.
        /// </param>
        /// <param name="offset">
        /// A byte offset relative to the origin parameter. 
        /// </param>
        /// <param name="seekOrigin">
        /// A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.
        /// </param>
        /// <param name="position">
        /// An output parameter to contain the current position in the current stream.
        /// </param>
        /// <returns>
        /// A reference to the target <see cref="Stream"/> instance this extension method is being called on.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="stream" /> is <c>null</c>. </exception>
        /// <exception cref="IOException">
        /// An I/O error occurs. 
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// The stream does not support seeking, such as if the stream is constructed from a pipe or console output. 
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// Methods were called after the stream was closed. 
        /// </exception>
        public static Stream FluentSeek(this Stream stream, long offset, SeekOrigin seekOrigin, out long position)
        {
            position = stream.VerifyArgument(nameof(stream)).IsNotNull().Value.Seek(offset, SeekOrigin.Begin);
            return stream;
        }
    }
}
