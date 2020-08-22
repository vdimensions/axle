using Axle.Conversion;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    internal sealed class BoxingConverter<T> : IConverter<CharSequence, object>
    {
        private readonly IConverter<CharSequence, T> _actualConverter;

        public BoxingConverter(IConverter<CharSequence, T> converter)
        {
            _actualConverter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }

        public object Convert(CharSequence source) => _actualConverter.Convert(source);

        public bool TryConvert(CharSequence source, out object target)
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
