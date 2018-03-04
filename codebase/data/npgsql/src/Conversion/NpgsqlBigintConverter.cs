using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlBigintConverter : NpgsqlSameTypeConverter<long?>
    {
        public NpgsqlBigintConverter() : base(DbType.Int64, NpgsqlDbType.Bigint, true) { }
    }
}