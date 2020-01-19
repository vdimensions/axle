using Axle.Conversion;
using Axle.Verification;

namespace Axle.Text.StructuredData.Binding
{
    internal sealed class BoxingConverter<T> : IConverter<string, object>
    {
        private readonly IConverter<string, T> _actualConverter;

        public BoxingConverter(IConverter<string, T> converter)
        {
            _actualConverter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }

        public object Convert(string source) => _actualConverter.Convert(source);

        public bool TryConvert(string source, out object target)
        {
            if (_actualConverter.TryConvert(source, out var result))
            {
                target = result;
                return true;
            }
            target = null;
            return false;
        }
    }
}
