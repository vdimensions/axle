using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlIntegerConverter : NpgsqlSameTypeConverter<int?>
    {
        public NpgsqlIntegerConverter() : base(DbType.Int32, NpgsqlDbType.Integer, true) { }
    }
}