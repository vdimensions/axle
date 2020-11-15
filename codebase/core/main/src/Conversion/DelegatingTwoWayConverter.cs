namespace Axle.Conversion
{
    /// <summary>
    /// An abstract class that implements the <see cref="ITwoWayConverter{TSource,TTarget}"/> interface
    /// and delegates the calls to an underlying <see cref="ITwoWayConverter{TSource,TTarget}"/> implementation.
    /// </summary>
    /// <typeparam name="TSrc">
    /// The source type of the conversion.
    /// </typeparam>
    /// <typeparam name="TDest">
    /// The destination type of the conversion.
    /// </typeparam>
    public abstract class DelegatingTwoWayConverter<TSrc, TDest> : ITwoWayConverter<TSrc, TDest>
    {
        private readonly ITwoWayConverter<TSrc, TDest> _impl;

        /// <summary>
        /// Initializes a new instance of the current <see cref="DelegatingTwoWayConverter{TSrc,TDest}"/> implementation.
        /// </summary>
        /// <param name="impl"></param>
        protected DelegatingTwoWayConverter(ITwoWayConverter<TSrc, TDest> impl) => _impl = impl;

        /// <inheritdoc />
        public TDest Convert(TSrc source) => _impl.Convert(source);

        /// <inheritdoc />
        public bool TryConvert(TSrc source, out TDest target) => _impl.TryConvert(source, out target);

        /// <inheritdoc />
        public TSrc ConvertBack(TDest obj) => _impl.ConvertBack(obj);

        /// <inheritdoc />
        public ITwoWayConverter<TDest, TSrc> Invert() => _impl.Invert();

        /// <inheritdoc />
        public bool TryConvertBack(TDest obj, out TSrc result) => _impl.TryConvertBack(obj, out result);
    }
}