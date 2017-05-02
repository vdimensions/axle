using System;

using Axle.Verification;


namespace Axle.Conversion.Sdk
{
    [Serializable]
    public sealed class ReverseConverter<TS, TD> : ITwoWayConverter<TS, TD>
    {
        private readonly ITwoWayConverter<TD, TS> converter;

        public ReverseConverter(ITwoWayConverter<TD, TS> converter)
        {
            this.converter = converter.VerifyArgument(nameof(converter)).IsNotNull().Value;
        }

        public TD Convert(TS source) { return converter.ConvertBack(source); }

        public TS ConvertBack(TD source) { return converter.Convert(source); }

        public IConverter<TD, TS> Invert() { return converter; }
        
        public bool TryConvert(TS source, out TD target) { return converter.TryConvertBack(source, out target); }

        public bool TryConvertBack(TD source, out TS target) { return converter.TryConvert(source, out target); }
    }
}