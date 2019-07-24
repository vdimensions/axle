using System.Diagnostics.CodeAnalysis;

namespace Axle.Configuration
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IIncludeExcludeElementCollection<T, TIncludeCollection, TExcludeCollection>
        where TIncludeCollection: IConfigurationElementCollection<T>
        where TExcludeCollection: IConfigurationElementCollection<T>
    {
        TIncludeCollection IncludeElements { get; }
        TExcludeCollection ExcludeElements { get; }
    }
}