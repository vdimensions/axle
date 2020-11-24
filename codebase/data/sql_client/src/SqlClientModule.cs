using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SqlClient
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [DbServiceProvider(Name = SqlServiceProvider.Name)]
    internal sealed class SqlClientModule : DatabaseServiceProviderModule
    {
        public SqlClientModule() : base(SqlServiceProvider.Instance) { } 
    }
}