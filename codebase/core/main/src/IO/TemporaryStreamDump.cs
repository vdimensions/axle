#if NETSTANDARD1_1_OR_NEWER || NETFRAMEWORK
using System;
using System.IO;
using Axle.IO.Extensions.Stream;
using Axle.Verification;

namespace Axle.IO
{
    /// <summary>
    /// A class representing a temporary dump of data stream to a file.
    /// The file is deleted when the current <see cref="TemporaryStreamDump"/> instance is disposed.
    /// </summary>
    public sealed class TemporaryStreamDump : IDisposable
    {
        /// <summary>
        /// Writes the contents of the provided <paramref name="stream"/> to a temporary file in the given <paramref name="location"/>.
        /// </summary>
        /// <param name="stream">
        /// The stream object to dump.
        /// </param>
        /// <param name="location">
        /// The location of the file to dump the stream into.
        /// </param>
        /// <param name="revertPosition">
        /// An optional parameter, indicating where the stream's position should be restored after dumping the data.
        /// </param>
        /// <returns>
        /// A <see cref="TemporaryStreamDump"/> instance pointing to the file containing the dumped data.
        /// </returns>
        public static TemporaryStreamDump Dump(Stream stream, string location, bool revertPosition = false)
        {
            StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(location, nameof(location)));
            Verifier.IsNotNull(Verifier.VerifyArgument(stream, nameof(stream)));
            if (!stream.CanRead)
            {
                throw new NotSupportedException("The specified input stream does not support reading.");
            }
            var pos = stream.Position;
            var file = new FileInfo(location);
            using (var fileStream = file.OpenWrite())
            {
                StreamExtensions.WriteTo(stream, fileStream, 16384);
                fileStream.Flush();
            }
            if (revertPosition && stream.CanSeek)
            {
                stream.Seek(pos, SeekOrigin.Begin);
            }
            return new TemporaryStreamDump(file);
        }

        /// <summary>
        /// Writes the contents of the provided <paramref name="stream"/> to a temporary file.
        /// </summary>
        /// <param name="stream">
        /// The stream object to dump.
        /// </param>
        /// <param name="revertPosition">
        /// An optional parameter, indicating where the stream's position should be restored after dumping the data.
        /// </param>
        /// <returns>
        /// A <see cref="TemporaryStreamDump"/> instance pointing to the file containing the dumped data.
        /// </returns>
        public static TemporaryStreamDump Dump(Stream stream, bool revertPosition = false) => Dump(stream, Path.GetTempFileName(), revertPosition);

        private TemporaryStreamDump(FileInfo file)
        {
            File = file;
        }

        void IDisposable.Dispose()
        {
            if (File.Exists)
            {
                File.Delete();
            }
        }

        /// <summary>
        /// Gets a <see cref="FileInfo"/> object representing the temporary file where the stream data was written.
        /// </summary>
        public FileInfo File { get; }
    }
}
#endif