using Axle.Verification;

namespace Axle.Conversion
{
    /// <summary>
    /// A class representing a combination of two separate
    /// <see cref="IConverter{TSource,TTarget}">one-way converter</see> instances.
    /// </summary>
    /// <typeparam name="T1">
    /// The source type to convert into a <typeparamref name="T2"/> (and back)
    /// </typeparam>
    /// <typeparam name="T2">
    /// The target type to convert a <typeparamref name="T1"/> instance to (and back)
    /// </typeparam>
    public sealed class CombinedConverter<T1, T2> : ITwoWayConverter<T1, T2>
    {
        private readonly IConverter<T1, T2> _primaryConverter;
        private readonly IConverter<T2, T1> _reverseConverter;

        /// <summary>
        /// Creates a new instance of the <see cref="CombinedConverter{T1,T2}"/> class.
        /// </summary>
        /// <param name="primaryConverter">
        /// The <see cref="IConverter{TSource,TTarget}"/> to be used for conversion from <typeparamref name="T1"/> to
        /// <typeparamref name="T2"/>
        /// </param>
        /// <param name="reverseConverter">
        /// The <see cref="IConverter{TSource,TTarget}"/> to be used for conversion from <typeparamref name="T2"/> to
        /// <typeparamref name="T1"/>
        /// </param>
        public CombinedConverter(IConverter<T1, T2> primaryConverter, IConverter<T2, T1> reverseConverter)
        {
            _primaryConverter = Verifier.IsNotNull(Verifier.VerifyArgument(primaryConverter, nameof(primaryConverter))).Value;
            _reverseConverter = Verifier.IsNotNull(Verifier.VerifyArgument(reverseConverter, nameof(reverseConverter))).Value;
        }

        /// <inheritdoc />
        T2 IConverter<T1, T2>.Convert(T1 source) => _primaryConverter.Convert(source);

        /// <inheritdoc />
        bool IConverter<T1, T2>.TryConvert(T1 source, out T2 target) => _primaryConverter.TryConvert(source, out target);

        /// <inheritdoc />
        IConverter<T2, T1> ITwoWayConverter<T1, T2>.Invert() => new CombinedConverter<T2, T1>(_reverseConverter, _primaryConverter);

        /// <inheritdoc />
        T1 ITwoWayConverter<T1, T2>.ConvertBack(T2 obj) => _reverseConverter.Convert(obj);

        /// <inheritdoc />
        bool ITwoWayConverter<T1, T2>.TryConvertBack(T2 obj, out T1 result) => _reverseConverter.TryConvert(obj, out result);
    }
}