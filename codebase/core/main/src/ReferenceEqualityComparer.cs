using System.Collections.Generic;


namespace Axle
{
    /// <summary>
    /// An implementation of the <see cref="IEqualityComparer{T}"/> interface
    /// that classifies two objects as equal only if they point to the same instance.
    /// The equality is determined internally by using the 
    /// <see cref="object.ReferenceEquals(object, object)"/> method.
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to compare.
    /// </typeparam>
    /// <seealso cref="object.ReferenceEquals(object, object)"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T: class
    {
        bool IEqualityComparer<T>.Equals(T x, T y) => ReferenceEquals(x, y);

        int IEqualityComparer<T>.GetHashCode(T obj) => obj.GetHashCode();
    }
}
