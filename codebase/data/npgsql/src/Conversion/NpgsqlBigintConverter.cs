using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlBigintConverter : NpgsqlSameTypeConverter<long?>
    {
        public NpgsqlBigintConverter() : base(DbType.Int64, NpgsqlDbType.Bigint, true) { }
    }
}