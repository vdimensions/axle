using System;
using Axle.Verification;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of a nullable
    /// <typeparamref name="T"/> value.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [Serializable]
    #endif
    public sealed class NullableParser<T> : AbstractParser<T?> where T: struct
    {
        private readonly IParser<T> _actualParser;

        /// <summary>
        /// Creates a new instance of the <see cref="NullableParser{T}"/> class.
        /// </summary>
        /// <param name="actualParser">
        /// The underlying <see cref="IParser{T}">parser</see> that is wrapped by the current 
        /// <see cref="NullableParser{T}"/> instance.
        /// </param>
        public NullableParser(IParser<T> actualParser)
        {
            _actualParser = Verifier.IsNotNull(Verifier.VerifyArgument(actualParser, nameof(actualParser))).Value;
        }

        /// <inheritdoc />
        protected override T? DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return value == null || value.Length == 0
                ? null as T?
                : _actualParser.Parse(value, formatProvider);
        }

        /// <inheritdoc />
        public override bool TryParse(CharSequence value, IFormatProvider formatProvider, out T? output)
        {
            output = null;
            if (value == null || value.Length == 0)
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