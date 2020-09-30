using System.Collections.Generic;

namespace Axle.Linq
{
    public interface ISequence<T> : IEnumerable<T>
    {
        int Count { get; }   
    }
}