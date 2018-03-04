using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlBooleanConverter : NpgsqlSameTypeConverter<bool?>
    {
        public NpgsqlBooleanConverter() : base(DbType.Boolean, NpgsqlDbType.Boolean) { }
    }
}