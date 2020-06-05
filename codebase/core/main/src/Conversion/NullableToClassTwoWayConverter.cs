using Axle.Verification;

namespace Axle.Conversion
{
    /// <summary>
    /// A wrapper around a <see cref="ITwoWayConverter{TSource,TTarget}"/> instance that enables conversion from
    /// the nullable version of source type from the original converter.
    /// </summary>
    /// <typeparam name="TSource">
    /// The source type from the original converter. This must be a value type.
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// The target type of the conversion, same as the target type of the original converter.
    /// This must be a reference type.
    /// </typeparam>
    public sealed class NullableToClassTwoWayConverter<TSource, TTarget> : ITwoWayConverter<TSource?, TTarget>
        where TSource: struct
        where TTarget: class
    {
        private readonly ITwoWayConverter<TSource, TTarget> _converter;

        /// <summary>
        /// Creates a new instance of the <see cref="NullableToClassTwoWayConverter{TSource, TTarget}"/>
        /// using the specified original <paramref name="converter"/>.
        /// </summary>
        /// <param name="converter"></param>
        public NullableToClassTwoWayConverter(ITwoWayConverter<TSource, TTarget> converter)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            _converter = converter;
        }

        TTarget IConverter<TSource?, TTarget>.Convert(TSource? source)
        {
            return source.HasValue ? _converter.Convert(source.Value) : null;
        }

        bool IConverter<TSource?, TTarget>.TryConvert(TSource? source, out TTarget target)
        {
            if (source.HasValue && _converter.TryConvert(source.Value, out var res))
            {
                target = res;
                return true;
            }
            target = null;
            return false;
        }
        
        TSource? ITwoWayConverter<TSource?, TTarget>.ConvertBack(TTarget source)
        {
            return source != null ? new TSource?(_converter.ConvertBack(source)) : null;
        }

        IConverter<TTarget, TSource?> ITwoWayConverter<TSource?, TTarget>.Invert() => new ReverseConverter<TTarget, TSource?>(this);

        bool ITwoWayConverter<TSource?, TTarget>.TryConvertBack(TTarget source, out TSource? target)
        {
            if (source != null && _converter.TryConvertBack(source, out var res))
            {
                target = res;
                return true;
            }
            target = null;
            return false;
        }
    }
}