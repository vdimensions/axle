using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SqlClient
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class SqlClientModule : DatabaseServiceProviderModule
    {
        public SqlClientModule() : base(SqlServiceProvider.Instance) { } 
    }
}