using System.Collections.Generic;


namespace Axle.Configuration
{
    public interface IConfigurationElementCollection<T> : IEnumerable<T>
    {
        T this[int index] { get; }
    }
}