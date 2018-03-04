using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    internal sealed class NpgsqlDoubleConverter : NpgsqlSameTypeConverter<double?>
    {
        public NpgsqlDoubleConverter() : base(DbType.Double, NpgsqlDbType.Double, true) { }
    }
}