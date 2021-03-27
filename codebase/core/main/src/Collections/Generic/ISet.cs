using System.Collections.Generic;
using Axle.Collections.ReadOnly;

namespace Axle.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed set of elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements.
    /// </typeparam>
    public interface ISet<T> : ICollection<T>, IReadOnlySet<T>
    {
    }
}