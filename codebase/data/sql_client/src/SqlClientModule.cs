using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SqlClient
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal sealed class SqlClientModule : DatabaseServiceProviderModule
    {
        public SqlClientModule() : base(SqlServiceProvider.Instance) { } 
    }
}