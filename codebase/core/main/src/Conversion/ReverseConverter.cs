using Axle.Verification;


namespace Axle.Conversion
{
    /// <summary>
    /// A reverse converter swaps the <see cref="IConverter{TSource,TTarget}.Convert"/> and <see cref="ITwoWayConverter{TSource,TTarget}.ConvertBack"/>
    /// methods and the <see cref="IConverter{TSource,TTarget}.TryConvert"/> and <see cref="ITwoWayConverter{TSource,TTarget}.TryConvertBack"/>
    /// of a provided <see cref="ITwoWayConverter{TSource,TTarget}"/> object.
    /// </summary>
    /// <typeparam name="TS">
    /// The source type for the reversed converter (same as the destination type of the underlying <see cref="ITwoWayConverter{TD,TS}"/>.
    /// </typeparam>
    /// <typeparam name="TD">
    /// The destination type for the reversed converter (same as the source type of the underlying <see cref="ITwoWayConverter{TD,TS}"/>.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ReverseConverter<TS, TD> : ITwoWayConverter<TS, TD>
    {
        private readonly ITwoWayConverter<TD, TS> _converter;

        /// <summary>
        /// Creates a new instance of the <see cref="ReverseConverter{TS,TD}"/> class.
        /// </summary>
        /// <param name="converter">
        /// The underlying <see cref="ITwoWayConverter{TSource,TTarget}">converter</see> object to be reversed.
        /// </param>
        public ReverseConverter(ITwoWayConverter<TD, TS> converter)
        {
            _converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }

        /// <inheritdoc />
        public TD Convert(TS source) => _converter.ConvertBack(source);

        /// <inheritdoc />
        public TS ConvertBack(TD source) => _converter.Convert(source);

        /// <inheritdoc />
        public IConverter<TD, TS> Invert() => _converter;

        /// <inheritdoc />
        public bool TryConvert(TS source, out TD target) => _converter.TryConvertBack(source, out target);

        /// <inheritdoc />
        public bool TryConvertBack(TD source, out TS target) => _converter.TryConvert(source, out target);
    }
}
