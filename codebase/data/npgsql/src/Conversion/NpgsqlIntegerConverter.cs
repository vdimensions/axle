using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class NpgsqlIntegerConverter : NpgsqlSameTypeConverter<int?>
    {
        public NpgsqlIntegerConverter() : base(DbType.Int32, NpgsqlDbType.Integer, true) { }
    }
}