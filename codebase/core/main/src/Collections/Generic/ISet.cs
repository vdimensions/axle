using System.Collections.Generic;

namespace Axle.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed set of elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements.
    /// </typeparam>
    #if NETSTANDARD || UNITY_2018_1_OR_NEWER
    public interface ISet<T> : ICollection<T>, IReadOnlySet<T>
    #else
    public interface ISet<T> : ICollection<T>
    #endif
    {
    }
}