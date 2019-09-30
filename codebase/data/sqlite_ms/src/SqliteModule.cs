using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Sqlite.Microsoft
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class SqliteModule : DatabaseServiceProviderModule
    {
        public SqliteModule() : base(SqliteServiceProvider.Instance){ }
    }
}