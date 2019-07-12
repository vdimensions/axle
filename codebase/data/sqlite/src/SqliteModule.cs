using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Sqlite
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class SqliteModule : DatabaseServiceProviderModule
    {
        public SqliteModule() : base(SqliteServiceProvider.Instance){ }
    }
}