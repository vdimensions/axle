using System.IO;

using Axle.Verification;


namespace Axle.Extensions.IO.Stream.Fluent
{
	using Stream = System.IO.Stream;

    public static class FluentStreamExtensions
    {
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
