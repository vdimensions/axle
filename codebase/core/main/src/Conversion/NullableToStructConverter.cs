using System;
using Axle.Verification;

namespace Axle.Conversion
{
    /// <summary>
    /// A wrapper around a <see cref="IConverter{TSource,TTarget}"/> instance that enables conversion from
    /// the nullable version of source type from the original converter.
    /// </summary>
    /// <typeparam name="TSource">
    /// The source type from the original converter.
    /// This must be a value type.
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// The target type of the conversion, same as the target type of the original converter.
    /// This must be a value type.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class NullableToStructConverter<TSource, TTarget> : IConverter<Nullable<TSource>, Nullable<TTarget>>
        where TSource: struct
        where TTarget: struct
    {
        private readonly IConverter<TSource, TTarget> _converter;

        /// <summary>
        /// Creates a new instance of the <see cref="NullableToStructConverter{TSource, TTarget}"/>
        /// using the specified original <paramref name="converter"/>.
        /// </summary>
        /// <param name="converter"></param>
        public NullableToStructConverter(IConverter<TSource, TTarget> converter)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            _converter = converter;
        }

        TTarget? IConverter<TSource?, TTarget?>.Convert(TSource? source)
        {
            return source.HasValue ? new TTarget?(_converter.Convert(source.Value)) : null;
        }

        bool IConverter<TSource?, TTarget?>.TryConvert(TSource? source, out TTarget? target)
        {
            if (source.HasValue && _converter.TryConvert(source.Value, out var res))
            {
                target = res;
                return true;
            }
            target = null;
            return false;
        }
    }
}