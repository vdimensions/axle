using Axle.Verification;


namespace Axle.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class ReverseConverter<TS, TD> : ITwoWayConverter<TS, TD>
    {
        private readonly ITwoWayConverter<TD, TS> _converter;

        public ReverseConverter(ITwoWayConverter<TD, TS> converter)
        {
            _converter = converter.VerifyArgument(nameof(converter)).IsNotNull().Value;
        }

        /// <inheritdoc />
        public TD Convert(TS source) { return _converter.ConvertBack(source); }

        /// <inheritdoc />
        public TS ConvertBack(TD source) { return _converter.Convert(source); }

        /// <inheritdoc />
        public IConverter<TD, TS> Invert() { return _converter; }

        /// <inheritdoc />
        public bool TryConvert(TS source, out TD target) { return _converter.TryConvertBack(source, out target); }

        /// <inheritdoc />
        public bool TryConvertBack(TD source, out TS target) { return _converter.TryConvert(source, out target); }
    }
}