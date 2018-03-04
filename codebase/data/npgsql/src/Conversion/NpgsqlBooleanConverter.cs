using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlBooleanConverter : NpgsqlSameTypeConverter<bool?>
    {
        public NpgsqlBooleanConverter() : base(DbType.Boolean, NpgsqlDbType.Boolean, true) { }
    }
}