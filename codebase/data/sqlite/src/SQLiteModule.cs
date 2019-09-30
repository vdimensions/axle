using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SQLite
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class SQLiteModule : DatabaseServiceProviderModule
    {
        public SQLiteModule() : base(SQLiteServiceProvider.Instance) { }
    }
}