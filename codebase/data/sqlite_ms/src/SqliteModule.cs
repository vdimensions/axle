using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Sqlite.Microsoft
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal sealed class SqliteModule : DatabaseServiceProviderModule
    {
        public SqliteModule() : base(SqliteServiceProvider.Instance){ }
    }
}