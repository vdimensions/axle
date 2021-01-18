using System.Diagnostics.CodeAnalysis;
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
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    internal sealed class CombinedConverter<T1, T2> : ITwoWayConverter<T1, T2>
    {
        private readonly IConverter<T1, T2> _sourceToDestinationConverter;
        private readonly IConverter<T2, T1> _destinationToSourceConverter;

        /// <summary>
        /// Creates a new instance of the <see cref="CombinedConverter{T1,T2}"/> class.
        /// </summary>
        /// <param name="sourceToDestinationConverter">
        /// The <see cref="IConverter{TSource,TTarget}"/> to be used for conversion from <typeparamref name="T1"/> to
        /// <typeparamref name="T2"/>
        /// </param>
        /// <param name="destinationToSourceConverter">
        /// The <see cref="IConverter{TSource,TTarget}"/> to be used for conversion from <typeparamref name="T2"/> to
        /// <typeparamref name="T1"/>
        /// </param>
        internal CombinedConverter(
            IConverter<T1, T2> sourceToDestinationConverter, 
            IConverter<T2, T1> destinationToSourceConverter)
        {
            _sourceToDestinationConverter = sourceToDestinationConverter;
            _destinationToSourceConverter = destinationToSourceConverter;
        }

        /// <inheritdoc />
        T2 IConverter<T1, T2>.Convert(T1 source) => _sourceToDestinationConverter.Convert(source);

        /// <inheritdoc />
        bool IConverter<T1, T2>.TryConvert(T1 source, out T2 target) 
            => _sourceToDestinationConverter.TryConvert(source, out target);

        /// <inheritdoc />
        ITwoWayConverter<T2, T1> ITwoWayConverter<T1, T2>.Invert() 
            => new CombinedConverter<T2, T1>(_destinationToSourceConverter, _sourceToDestinationConverter);

        /// <inheritdoc />
        T1 ITwoWayConverter<T1, T2>.ConvertBack(T2 obj) => _destinationToSourceConverter.Convert(obj);

        /// <inheritdoc />
        bool ITwoWayConverter<T1, T2>.TryConvertBack(T2 obj, out T1 result) 
            => _destinationToSourceConverter.TryConvert(obj, out result);
    }
    
    /// <summary>
    /// A static class containing utilities for creating combined converters
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class CombinedConverter
    {
        /// <summary>
        /// Creates a <see cref="ITwoWayConverter{TSource,TTarget}"/> instance from the provided
        /// one-way converters.
        /// </summary>
        /// <param name="sourceToDestinationConverter">
        /// A <see cref="IConverter{TSource,TTarget}"/> that will be used to convert values of the
        /// source type <typeparamref name="TSrc"/> to values of the destination type <typeparamref name="TDest"/>. 
        /// </param>
        /// <param name="destinationToSourceConverter">
        /// A <see cref="IConverter{TSource,TTarget}"/> that will be used to convert values of the
        /// destination type <typeparamref name="TDest"/> back to values of the source type <typeparamref name="TSrc"/>. 
        /// </param>
        /// <typeparam name="TSrc">
        /// The source type to convert from.
        /// </typeparam>
        /// <typeparam name="TDest">
        /// The destination type to convert to.
        /// </typeparam>
        /// <returns>
        /// An <see cref="ITwoWayConverter{TSource,TTarget}"/> that delegates the conversion between the
        /// <typeparamref name="TSrc"/> and <typeparamref name="TDest"/> to the provided by the
        /// <paramref name="sourceToDestinationConverter"/> and <paramref name="destinationToSourceConverter"/> one-way
        /// converters.
        /// </returns>
        public static ITwoWayConverter<TSrc, TDest> Create<TSrc, TDest>(
            IConverter<TSrc, TDest> sourceToDestinationConverter, 
            IConverter<TDest, TSrc> destinationToSourceConverter)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(sourceToDestinationConverter, nameof(sourceToDestinationConverter)));
            Verifier.IsNotNull(Verifier.VerifyArgument(destinationToSourceConverter, nameof(destinationToSourceConverter)));
            return new CombinedConverter<TSrc, TDest>(sourceToDestinationConverter, destinationToSourceConverter);
        }
    }
}