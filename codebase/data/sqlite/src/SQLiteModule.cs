using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SQLite
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal sealed class SQLiteModule : DatabaseServiceProviderModule
    {
        public SQLiteModule() : base(SQLiteServiceProvider.Instance) { }
    }
}