namespace Axle.Conversion
{
    /// <summary>
    /// An identity converter, that is, a <see cref="IConverter{T, T}"/> implementation that returns 
    /// the object instance being passed for conversion without changing it. 
    /// </summary>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class IdentityConverter<T> : ITwoWayConverter<T, T>
    {
        /// <inheritdoc />
        public T Convert(T source) { return source; }

        /// <inheritdoc />
        public T ConvertBack(T target) { return target; }

        /// <inheritdoc />
        public IConverter<T, T> Invert() { return this; }

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
        public bool TryConvertBack(T source, out T target)  { return TryConvert(source, out target); }
    }
}

