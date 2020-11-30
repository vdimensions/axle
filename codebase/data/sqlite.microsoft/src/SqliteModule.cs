using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Sqlite.Microsoft
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [DbServiceProvider(Name = SqliteServiceProvider.Name)]
    internal sealed class SqliteModule : DbServiceProvider
    {
        public SqliteModule() : base(SqliteServiceProvider.Instance){ }
    }
}