using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Configuration
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IIncludeExcludeElementCollection<T, TIncludeCollection, TExcludeCollection>
        where TIncludeCollection: IEnumerable<T>
        where TExcludeCollection: IEnumerable<T>
    {
        TIncludeCollection IncludeElements { get; }
        TExcludeCollection ExcludeElements { get; }
    }
    public interface IIncludeExcludeElementCollection<T> : IIncludeExcludeElementCollection<T, IEnumerable<T>, IEnumerable<T>>
    {
    }
}