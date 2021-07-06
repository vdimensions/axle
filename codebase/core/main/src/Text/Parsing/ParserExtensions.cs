using Axle.Verification;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A static class holding extension methods for the <see cref="IParser{T}"/> type.
    /// </summary>
    public static class ParserExtensions
    {
        /// <summary>
        /// Gets a nullable type parser instance derived from the provided <paramref name="parser"/>.
        /// </summary>
        /// <typeparam name="T">The type of values this parser can handle.</typeparam>
        /// <param name="parser">
        /// The underlying <see cref="IParser{T}"/> which is used to parse non-empty string representations of 
        /// <typeparamref name="T"/>
        /// </param>
        /// <returns>
        /// A nullable type parser instance derived from the provided <paramref name="parser"/>.
        /// </returns>
        public static IParser<T?> GetNullableParser<T>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            IParser<T> parser) where T: struct
        {
            return new NullableParser<T>(Verifier.IsNotNull(Verifier.VerifyArgument(parser, nameof(parser))).Value);
        }
    }
}
