using Axle.Data.Resources;
using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    [Utilizes("Axle.Data.SqlClient.SqlClientModule, Axle.Data.SqlClient")]
    [Utilizes("Axle.Data.Npgsql.NpgsqlModule, Axle.Data.Npgsql")]
    [Utilizes("Axle.Data.Sqlite.SqliteModule, Axle.Data.Sqlite")]
    [Utilizes("Axle.Data.MySql.MySqlModule, Axle.Data.MySql")]
    [Requires(typeof(SqlScriptSourceModule))]
    internal sealed class DataModule
    {

    }
}