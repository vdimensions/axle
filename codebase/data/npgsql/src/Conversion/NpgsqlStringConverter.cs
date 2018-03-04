using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlStringConverter : NpgsqlSameTypeConverter<string>
    {
        public NpgsqlStringConverter() : base(DbType.String, NpgsqlDbType.Varchar, true) { }
    }
}