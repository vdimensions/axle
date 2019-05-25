using System;

using Axle.Verification;


namespace Axle.Conversion.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a nullable
    /// <typeparamref name="T"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableParser<T> : AbstractParser<T?> where T: struct
    {
        private readonly IParser<T> _actualParser;

        public NullableParser(IParser<T> actualParser)
        {
            _actualParser = Verifier.IsNotNull(Verifier.VerifyArgument(actualParser, nameof(actualParser))).Value;
        }

        /// <inheritdoc />
        protected override T? DoParse(string value, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(value)
                ? null as T?
                : _actualParser.Parse(value, formatProvider);
        }

        public override bool TryParse(string value, IFormatProvider formatProvider, out T? output)
        {
            output = null;
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (!_actualParser.TryParse(value, formatProvider, out var res))
            {
                return false;
            }

            output = res;
            return true;
        }
    }
}