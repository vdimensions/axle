using System;


namespace Axle.Conversion
{
    /// <summary>
    /// An identity converter, that is, a <see cref="IConverter{T, T}"/> implementation that returns 
    /// the object instance being passed for conversion without changing it. 
    /// </summary>
    #if !netstandard
    [Serializable]
    #endif
    public sealed class IdentityConverter<T> : ITwoWayConverter<T, T>
    {
        public T Convert(T source) { return source; }

        public T ConvertBack(T target) { return target; }

        public IConverter<T, T> Invert() { return this; }

        public bool TryConvert(T source, out T target) 
        {
            target = source;
            return true;
        }

        public bool TryConvertBack(T source, out T target)  { return TryConvert(source, out target); }
    }
}

