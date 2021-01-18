namespace Axle.Conversion
{
    /// <summary>
    /// An abstract class that implements the <see cref="IConverter{TSource,TTarget}"/> interface
    /// and delegates the calls to an underlying <see cref="IConverter{TSource,TTarget}"/> implementation.
    /// </summary>
    /// <typeparam name="TSrc">
    /// The source type of the conversion.
    /// </typeparam>
    /// <typeparam name="TDest">
    /// The destination type of the conversion.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
    [System.Serializable]
    #endif
    public abstract class DelegatingConverter<TSrc, TDest> : IConverter<TSrc, TDest>
    {
        private readonly IConverter<TSrc, TDest> _impl;

        /// <summary>
        /// Initializes a new instance of the current <see cref="DelegatingConverter{TSrc,TDest}"/> implementation.
        /// </summary>
        /// <param name="impl"></param>
        protected DelegatingConverter(IConverter<TSrc, TDest> impl) => _impl = impl;

        /// <inheritdoc />
        public virtual TDest Convert(TSrc source) => _impl.Convert(source);

        /// <inheritdoc />
        public virtual bool TryConvert(TSrc source, out TDest target) => _impl.TryConvert(source, out target);
    }
}