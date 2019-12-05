#if NETSTANDARD1_3_OR_NEWER || NET35_OR_NEWER
using Axle.Verification;
using System;
using System.Collections.Concurrent;

namespace Axle.Conversion.Binding
{
    public sealed class BindingConverter : IBindingConverter
    {
        private readonly IBindingConverter _fallbackConverter = new DefaultBindingConverter();
        private readonly ConcurrentDictionary<Type, IConverter<string, object>> _converters = new ConcurrentDictionary<Type, IConverter<string, object>>();

        public void RegisterConverter<T>(IConverter<string, T> converter)
        {
            var boxingConverter = new BoxingConverter<T>(converter);
            _converters.AddOrUpdate(
                typeof(T),
                boxingConverter,
                (_, c) => boxingConverter);
        }

        /// <inheritdoc />
        public bool TryConvertMemberValue(string rawValue, Type targetType, out object boundValue)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(targetType, nameof(targetType)));
            return _converters.TryGetValue(targetType, out var converter)
                ? converter.TryConvert(rawValue, out boundValue)
                : _fallbackConverter.TryConvertMemberValue(rawValue, targetType, out boundValue);
        }
    }
}
#endif