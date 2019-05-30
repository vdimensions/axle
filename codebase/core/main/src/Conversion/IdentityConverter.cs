namespace Axle.Conversion
{
    /// <summary>
    /// An identity converter, that is, a <see cref="IConverter{T, T}"/> implementation that returns
    /// the object instance being passed for conversion without changing it.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class IdentityConverter<T> : ITwoWayConverter<T, T>
    {
        /// <inheritdoc />
        public T Convert(T source) => source;

        /// <inheritdoc />
        public T ConvertBack(T target) => target;

        /// <inheritdoc />
        public IConverter<T, T> Invert() => this;

        /// <inheritdoc />
        /// <remarks>
        /// This method returns the current instance.
        /// </remarks>
        public bool TryConvert(T source, out T target) 
        {
            target = source;
            return true;
        }

        /// <inheritdoc />
        public bool TryConvertBack(T source, out T target) => TryConvert(source, out target);
    }
}
