using System.Collections.Generic;

namespace Axle.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed set of elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements.
    /// </typeparam>
    #if NETSTANDARD
    public interface ISet<T> : ICollection<T>, IReadOnlySet<T>
    #else
    public interface ISet<T> : ICollection<T>
    #endif
    {
    }
}