namespace Axle.Conversion
{
    /// <summary>
    /// Similar to the identity function in mathematics - which returns the value passed in,
    /// an identity converter is an implementation of the <see cref="IConverter{T, T}"/> interface
    /// that returns the same object instance which was passed for conversion.
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
